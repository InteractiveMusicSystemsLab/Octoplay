#!/bin/bash

# Install appdmg globally with: npm install -g appdmg
# Make executable with: chmod +x rebuild-dmg.sh
# Run with: ./rebuild-dmg.sh

set -e

APP_NAME="${APP_NAME:-Octoplay}"
OUTPUT_DIR="${OUTPUT_DIR:-_builds}"
APP_BUNDLE="${APP_BUNDLE:-${OUTPUT_DIR}/${APP_NAME}.app}"
CONFIG="${CONFIG:-appdmg-config.json}"

TEAM_ID="${TEAM_ID:-HSAYDGFEVC}"
IDENTITY="${IDENTITY:-Developer ID Application: Clear Blue Media LLC (${TEAM_ID})}"

if [ -f ./signing.env ]; then
  # shellcheck disable=SC1091
  source ./signing.env
fi

if [ -z "$NOTARY_PROFILE" ]; then
  echo "❌ NOTARY_PROFILE is not set. Add it to signing.env or export it."
  exit 1
fi

if [ ! -d "$APP_BUNDLE" ]; then
  echo "❌ App bundle not found at: $APP_BUNDLE"
  exit 1
fi

if [ ! -f "$CONFIG" ]; then
  echo "❌ DMG layout config not found: $CONFIG"
  exit 1
fi

VERSION="unknown"
if [ -f "$APP_BUNDLE/Contents/Info.plist" ]; then
  VERSION=$(/usr/libexec/PlistBuddy -c "Print :CFBundleShortVersionString" "$APP_BUNDLE/Contents/Info.plist" 2>/dev/null || echo "unknown")
fi

mkdir -p "$OUTPUT_DIR"

DMG_NAME="${APP_NAME}-${VERSION}.dmg"
DMG_PATH="${OUTPUT_DIR}/${DMG_NAME}"

echo "🧼 Removing any previous DMG..."
rm -f "$DMG_PATH"

echo "📀 Rebuilding DMG using appdmg..."
TMP_BASE="$(mktemp -t appdmg-config.XXXXXX)"
TMP_CONFIG="${TMP_BASE}.json"
mv "$TMP_BASE" "$TMP_CONFIG"
python3 - <<PY
import json
import os

config_path = "${CONFIG}"
app_path = os.path.abspath("${APP_BUNDLE}")

with open(config_path, "r", encoding="utf-8") as f:
    data = json.load(f)

for key in ("icon", "background"):
    if key in data and isinstance(data[key], str):
        data[key] = os.path.abspath(data[key])

if data.get("contents") and isinstance(data["contents"], list):
    for entry in data["contents"]:
        if entry.get("type") == "file":
            entry["path"] = app_path

with open("${TMP_CONFIG}", "w", encoding="utf-8") as f:
    json.dump(data, f, indent=2)
PY

appdmg "$TMP_CONFIG" "$DMG_PATH"

echo "🧾 Verifying DMG was created..."
if [ ! -f "$DMG_PATH" ]; then
  echo "❌ DMG not found: $DMG_PATH"
  exit 1
fi

echo "🔏 Signing DMG..."
codesign --timestamp --sign "$IDENTITY" "$DMG_PATH"

echo "☁️ Submitting to Apple for notarization..."
xcrun notarytool submit "$DMG_PATH" \
  --keychain-profile "$NOTARY_PROFILE" \
  --wait

echo "📎 Stapling ticket to DMG..."
xcrun stapler staple "$DMG_PATH"

echo "✅ DMG rebuilt, signed, notarized, and stapled: $DMG_PATH"

if [ -n "$TMP_CONFIG" ] && [ -f "$TMP_CONFIG" ]; then
  rm -f "$TMP_CONFIG"
fi
