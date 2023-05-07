DEPLOYDIR = $(HOME)/bin

default:
	@echo 'Targets:'
	@echo '  build'
	@echo '  release'
	@echo '  deploy'

deploy: release
	cp ./target/release/media_multi $(DEPLOYDIR)

build:
	cargo build

release:
	cargo build --release
