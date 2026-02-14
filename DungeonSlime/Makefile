.PHONY: clean build-arm64 build-x64 app release help

# Configuration
PROJECT_NAME := DungeonSlime
CSPROJ := $(PROJECT_NAME).csproj
BUILD_DIR := bin/Release
PUBLISH_ARM64 := $(BUILD_DIR)/net9.0/osx-arm64/publish
PUBLISH_X64 := $(BUILD_DIR)/net9.0/osx-x64/publish
CONTENT_ARM64 := $(BUILD_DIR)/net9.0/osx-arm64/Content
CONTENT_X64 := $(BUILD_DIR)/net9.0/osx-x64/Content
APP_DIR := $(BUILD_DIR)/$(PROJECT_NAME).app
MACOS_DIR := $(APP_DIR)/Contents/MacOS
RESOURCES_DIR := $(APP_DIR)/Contents/Resources
ICON_SRC := Icon.ico
ICON_PNG := $(BUILD_DIR)/Icon.png
ICONSET_DIR := $(BUILD_DIR)/$(PROJECT_NAME).iconset
ICNS_FILE := $(RESOURCES_DIR)/$(PROJECT_NAME).icns

help:
	@echo "Available targets:"
	@echo "  make release-macos-arm64 - Build arm64 and create distributable .app"
	@echo "  make build-arm64 - Build arm64 release only"
	@echo "  make build-x64   - Build x64 release only"
	@echo "  make app         - Create app bundle from existing builds"
	@echo "  make clean       - Remove all build artifacts"
	@echo "  make install-macos-arm64 - Copy app to ~/Applications (for testing)"

clean:
	@echo "Cleaning release builds..."
	rm -rf $(BUILD_DIR)

build-arm64:
	@echo "Building arm64 release..."
	dotnet publish -c Release -r osx-arm64 -p:PublishReadyToRun=false -p:TieredCompilation=false --self-contained

build-x64:
	@echo "Building x64 release..."
	dotnet publish -c Release -r osx-x64 -p:PublishReadyToRun=false -p:TieredCompilation=false --self-contained

$(APP_DIR)/Contents/Resources:
	@mkdir -p $(APP_DIR)/Contents/Resources

$(MACOS_DIR): $(APP_DIR)/Contents/Resources
	@mkdir -p $(MACOS_DIR)

$(MACOS_DIR)/DungeonSlime: $(MACOS_DIR) build-arm64
	@echo "Copying arm64 binaries..."
	cp -R $(PUBLISH_ARM64)/* $(MACOS_DIR)/
	@echo "Copying content to both MacOS and Resources..."
	mkdir -p $(RESOURCES_DIR)
	rm -rf $(MACOS_DIR)/Content $(RESOURCES_DIR)/Content 2>/dev/null || true
	cp -R $(CONTENT_ARM64) $(MACOS_DIR)/
	cp -R $(CONTENT_ARM64) $(RESOURCES_DIR)/
	@chmod +x $(MACOS_DIR)/DungeonSlime

$(ICON_PNG): $(ICON_SRC)
	@echo "Converting icon from .ico to .png..."
	@mkdir -p $(BUILD_DIR)
	magick "$(ICON_SRC)[0]" -resize 1024x1024 $(ICON_PNG)

$(ICONSET_DIR): $(ICON_PNG)
	@echo "Creating icon set..."
	@mkdir -p $(ICONSET_DIR)
	sips -z 16 16 $(ICON_PNG) --out $(ICONSET_DIR)/icon_16x16.png
	sips -z 32 32 $(ICON_PNG) --out $(ICONSET_DIR)/icon_16x16@2x.png
	sips -z 32 32 $(ICON_PNG) --out $(ICONSET_DIR)/icon_32x32.png
	sips -z 64 64 $(ICON_PNG) --out $(ICONSET_DIR)/icon_32x32@2x.png
	sips -z 128 128 $(ICON_PNG) --out $(ICONSET_DIR)/icon_128x128.png
	sips -z 256 256 $(ICON_PNG) --out $(ICONSET_DIR)/icon_128x128@2x.png
	sips -z 256 256 $(ICON_PNG) --out $(ICONSET_DIR)/icon_256x256.png
	sips -z 512 512 $(ICON_PNG) --out $(ICONSET_DIR)/icon_256x256@2x.png
	sips -z 512 512 $(ICON_PNG) --out $(ICONSET_DIR)/icon_512x512.png
	sips -z 1024 1024 $(ICON_PNG) --out $(ICONSET_DIR)/icon_512x512@2x.png

$(ICNS_FILE): $(ICONSET_DIR) $(APP_DIR)/Contents/Resources
	@echo "Creating .icns file..."
	iconutil -c icns $(ICONSET_DIR) --output $(ICNS_FILE)

$(APP_DIR)/Contents/Info.plist:
	@echo "Creating Info.plist..."
	@mkdir -p $(APP_DIR)/Contents
	@printf '<?xml version="1.0" encoding="UTF-8"?>\n<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">\n<plist version="1.0">\n<dict>\n\t<key>CFBundleDevelopmentRegion</key>\n\t<string>en</string>\n\t<key>CFBundleExecutable</key>\n\t<string>DungeonSlime</string>\n\t<key>CFBundleIdentifier</key>\n\t<string>com.example.dungeonslime</string>\n\t<key>CFBundleInfoDictionaryVersion</key>\n\t<string>6.0</string>\n\t<key>CFBundleName</key>\n\t<string>Dungeon Slime</string>\n\t<key>CFBundlePackageType</key>\n\t<string>APPL</string>\n\t<key>CFBundleShortVersionString</key>\n\t<string>1.0</string>\n\t<key>CFBundleVersion</key>\n\t<string>1</string>\n\t<key>NSHighResolutionCapable</key>\n\t<true/>\n\t<key>NSHumanReadableCopyright</key>\n\t<string>Copyright © 2026. All rights reserved.</string>\n\t<key>CFBundleIconFile</key>\n\t<string>DungeonSlime</string>\n</dict>\n</plist>' > $(APP_DIR)/Contents/Info.plist

app: $(MACOS_DIR)/DungeonSlime $(ICNS_FILE) $(APP_DIR)/Contents/Info.plist
	@echo "Verifying icon in bundle..."
	@if [ ! -f "$(ICNS_FILE)" ]; then \
		echo "ERROR: Icon file not created!"; \
		exit 1; \
	fi
	@echo "Setting app bundle attributes..."
	@SetFile -a B $(APP_DIR)
	@SetFile -t APPL $(APP_DIR)
	@echo "App bundle created at: $(APP_DIR)"
	@echo "Icon location: $(ICNS_FILE)"

release-macos-arm64: clean build-arm64 app
	@echo ""
	@echo "==================================="
	@echo "Release complete!"
	@echo "App location: $(APP_DIR)"
	@echo "==================================="

install-macos-arm64: release-macos-arm64
	@echo "Copying app to ~/Applications for testing..."
	cp -R $(APP_DIR) ~/Applications/
	@echo "Installed at: ~/Applications/$(PROJECT_NAME).app"

.PHONY: help clean build-arm64 build-x64 app release-macos-arm64 install-macos-arm64
