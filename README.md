It's like Simon, but with diatonic pitches...and an octopus...
https://clearbluemedia.com/octoplay/

Developed with Unity. To demo, open the scene marked "Octoplay".

## Mac Build + Sign

1) Build the macOS standalone app from Unity to `_builds/Octoplay.app`.
2) Run:
   `./build-and-sign.sh`

### One-time setup (required)

- Create an app-specific password at https://appleid.apple.com.
- Store notary credentials in your keychain:
  `xcrun notarytool store-credentials "octoplay-notary" --apple-id "you@appleid.com" --team-id "YOUR_TEAM_ID" --password "APP_SPECIFIC_PASSWORD"`
- Ensure the following values are set for your signing identity:
  - `TEAM_ID` and `IDENTITY` in `codesign-octoplay.sh` and `rebuild-dmg.sh`
  - Optionally add overrides in `signing.env` (e.g., `NOTARY_PROFILE=octoplay-notary`)

### Notes

- The scripts expect the app bundle at `_builds/Octoplay.app` and will place the signed DMG in `_builds/`.
- If you change the app name, set `APP_NAME` and/or `APP_BUNDLE` when running the script.


## Credits
Original authors:
- Dan Manzo (@danmanzo) and V.J. Manzo (@vjmanzo)

## License
Released under the AGPLv3 license.

## Contributing

Navigate to the [CONTRIBUTING.md](./CONTRIBUTING.md) file for guidelines on how to contribute to the project.