#!/bin/bash

# Make executable with: chmod +x build-and-sign.sh
# Run with: ./build-and-sign.sh

set -e

APP_NAME="${APP_NAME:-Octoplay}"
OUTPUT_DIR="${OUTPUT_DIR:-_builds}"
APP_BUNDLE="${APP_BUNDLE:-${OUTPUT_DIR}/${APP_NAME}.app}"

echo "📦 Using app bundle: $APP_BUNDLE"
if [ ! -d "$APP_BUNDLE" ]; then
  echo "❌ App bundle not found at: $APP_BUNDLE"
  echo "Set APP_BUNDLE to your Unity build output, e.g.:"
  echo "  APP_BUNDLE=\"/path/to/Octoplay.app\" ./build-and-sign.sh"
  exit 1
fi

echo "🔏 Codesigning and notarizing the app..."
./codesign-octoplay.sh

echo "📀 Building, signing, and notarizing DMG..."
./rebuild-dmg.sh

echo "✅ All steps completed successfully!"
echo "Output folder: $OUTPUT_DIR"
