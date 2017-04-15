module LinkParserTests

open Xunit
open HttpHeader

[<Fact>]
let ``parse url and next should return correct url and direction``() =
  let toParse = "<https://api.example.foobar.com/this/path/is/cool>; rel=\"next\""

  let expectedUrl = "https://api.example.foobar.com/this/path/is/cool"
  let expectedDirection = LinkParser.LinkDirection.Next

  let result = LinkParser.Parse toParse
  let onlySomeResult = Array.choose id result
  let (url, direction) = Array.head onlySomeResult


  Assert.NotEmpty result
  Assert.NotEmpty onlySomeResult
  Assert.Equal(1, (Array.length onlySomeResult))
  Assert.Equal(expectedUrl, url)
  Assert.Equal(expectedDirection, direction)

