# Containerized C# API 
.NET6 API for http client and sample user service, including health check endpoint. Written and guided by test driven development, giving meaningful test coverage to the API as a whole.

### Building docker image : 
``` docker build --rm -t basic-api:latest .```

### Docker Run environment :
``` docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 basic-api ```