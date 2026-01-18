#!/bin/bash

# Make executable with: chmod +x codesign-octoplay.sh
# Run with: ./codesign-octoplay.sh

set -e

APP_NAME="${APP_NAME:-Octoplay}"
OUTPUT_DIR="${OUTPUT_DIR:-_builds}"
APP_BUNDLE="${APP_BUNDLE:-${OUTPUT_DIR}/${APP_NAME}.app}"
ENTITLEMENTS_PATH="${ENTITLEMENTS_PATH:-./entitlements.plist}"
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

echo "🧹 Removing legacy CodeResources and old signatures..."
rm -f "$APP_BUNDLE/Contents/CodeResources"
rm -rf "$APP_BUNDLE/Contents/_CodeSignature"

VERSION="unknown"
if [ -f "$APP_BUNDLE/Contents/Info.plist" ]; then
  VERSION=$(/usr/libexec/PlistBuddy -c "Print :CFBundleShortVersionString" "$APP_BUNDLE/Contents/Info.plist" 2>/dev/null || echo "unknown")
fi

mkdir -p "$OUTPUT_DIR"

NOTARIZE_ZIP="${OUTPUT_DIR}/${APP_NAME}-${VERSION}-NOTARIZE.zip"
FINAL_ZIP="${OUTPUT_DIR}/${APP_NAME}-${VERSION}-mac.zip"

echo "🧼 Cleaning previous zip files..."
rm -f "$NOTARIZE_ZIP" "$FINAL_ZIP"

SIGN_ARGS=(--force --timestamp --options runtime --sign "$IDENTITY")
if [ -f "$ENTITLEMENTS_PATH" ]; then
  SIGN_ARGS+=(--entitlements "$ENTITLEMENTS_PATH")
fi

echo "🔏 Signing nested frameworks..."
while IFS= read -r -d '' FRAMEWORK; do
  echo "Signing $FRAMEWORK"
  codesign "${SIGN_ARGS[@]}" "$FRAMEWORK"
done < <(find "$APP_BUNDLE/Contents/Frameworks" -type d -name "*.framework" -print0 2>/dev/null || true)

echo "🔏 Signing bundles..."
while IFS= read -r -d '' BUNDLE; do
  echo "Signing $BUNDLE"
  codesign "${SIGN_ARGS[@]}" "$BUNDLE"
done < <(find "$APP_BUNDLE/Contents" -type d -name "*.bundle" -print0 2>/dev/null || true)

echo "🔏 Signing dylibs and executables..."
while IFS= read -r -d '' BIN; do
  echo "Signing $BIN"
  codesign "${SIGN_ARGS[@]}" "$BIN"
done < <(
  find "$APP_BUNDLE/Contents" -type f \( -name "*.dylib" -o -name "*.so" -o -path "*/MacOS/*" \) -print0 2>/dev/null || true
)

echo "🔏 Signing main app bundle..."
codesign "${SIGN_ARGS[@]}" --deep "$APP_BUNDLE"

if [ -f "$APP_BUNDLE/Contents/CodeResources" ]; then
  echo "❌ Unexpected legacy CodeResources detected after signing."
  echo "Remove $APP_BUNDLE/Contents/CodeResources and re-sign."
  exit 1
fi

echo "🔍 Verifying codesign..."
codesign --verify --deep --strict --verbose=2 "$APP_BUNDLE"

echo "📦 Creating zip for notarization..."
ditto -c -k --keepParent "$APP_BUNDLE" "$NOTARIZE_ZIP"

echo "☁️ Submitting to Apple for notarization..."
xcrun notarytool submit "$NOTARIZE_ZIP" \
  --keychain-profile "$NOTARY_PROFILE" \
  --wait

echo "📎 Stapling ticket to .app..."
xcrun stapler staple "$APP_BUNDLE"

echo "📦 Creating stapled zip for distribution..."
ditto -c -k --keepParent "$APP_BUNDLE" "$FINAL_ZIP"

echo "🧹 Cleaning up notarization zip..."
rm -f "$NOTARIZE_ZIP"

echo "✅ Done!"
echo "Notarized app bundle: $APP_BUNDLE"
echo "Final stapled zip for distribution: $FINAL_ZIP"
