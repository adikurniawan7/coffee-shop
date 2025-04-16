# Coffee Shop API

This HTTP API controls an imaginary internet-connected coffee machine.
Coffee shop API contains one endpoint `GET /brew-coffee`. When this endpoint is called, it returns a 200 OK status code  with a status message and the current date/time in the response body as a JSON object, with  the date/time formatted as an ISO-8601 value e.g.  

`{` 
 `“message”: “Your piping hot coffee is ready”,` 
 `“prepared”: “2021-02-03T11:56:24+0900”` 
`};`
 
On every fifth call to the endpoint defined in #1, the endpoint should return `503 Service Unavailable` with an empty response body, to signify that the coffee machine is out of  coffee; 

If the date is April 1st, then all calls to the endpoint defined should return `418 I’m a teapot` instead, with an empty response body, to signify that the endpoint is not brewing coffee today (see https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/418)

## Requirements

.NET Core 9.0, Visual Studio 2022 (Recommended)

## How do I get started?

1. Download this project from GitHub 
   https://github.com/adikurniawan7/coffee-shop
2. Open project in Visual Studio
3. Run (F5)

## How do I test this project?

The easiest way to test this project is to run `CoffeeShop.Test` project in Visual Studio