VERSION 0.8

dotnet-sdk:
	FROM mcr.microsoft.com/dotnet/sdk:8.0
	RUN apt-get update
	RUN apt-get install -y jq
	WORKDIR /work/.config
	COPY .config/dotnet-tools.json .
	WORKDIR /work
	RUN dotnet tool restore

get-msbuild-chain-files:
	FROM busybox
	WORKDIR /work
	COPY *.props .
	COPY *.targets .
	WORKDIR /work/src
	COPY src/*.props .
	WORKDIR /work/tests
	COPY tests/*.props .
	WORKDIR /work
	SAVE ARTIFACT .
