LINUX-RID = linux-x64
MACOS-RID = osx-x64
WINDOWS-RID = win-x64

LINUX-ARCHIVE = media-multi-linux.tar.gz
MACOS-ARCHIVE = media-multi-macos.zip
WINDOWS-ARCHIVE = media-multi-windows.zip

default:
	@echo 'Targets:'
	@echo '  build'
	@echo '  deploy'
	@echo '  clean'

build: clean build-linux build-mac build-windows

build-linux:
	dotnet publish -c Release -r $(LINUX-RID) --self-contained true /p:PublishSingleFile=true

build-mac:
	dotnet publish -c Release -r $(MACOS-RID) --self-contained true /p:PublishSingleFile=true

build-windows:
	dotnet publish -c Release -r $(WINDOWS-RID) --self-contained true /p:PublishSingleFile=true

deploy: deploy-linux

deploy-linux: build-linux
	cp ./bin/Release/net7.0/$(LINUX-RID)/publish/media-multi $(HOME)/bin

bundle: build
	tar -czf $(LINUX-ARCHIVE) ./bin/Release/net7.0/$(LINUX-RID)/publish/media-multi
	zip $(MACOS-ARCHIVE) ./bin/Release/net7.0/$(MACOS-RID)/publish/media-multi
	zip $(WINDOWS-ARCHIVE) ./bin/Release/net7.0/$(WINDOWS-RID)/publish/media-multi.exe

clean:
	dotnet clean
	-rm -f $(LINUX-ARCHIVE)
	-rm -f $(MACOS-ARCHIVE)
	-rm -f $(WINDOWS-ARCHIVE)
