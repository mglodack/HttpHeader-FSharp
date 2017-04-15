module LinkParserTests

open Xunit
open HttpHeader

[<Fact>]
let ``Relation of value next should return Next LinkDirection``() =
  let relation = "rel=\"next\""

  Assert.Equal(LinkParser.LinkDirection.Next, (LinkParser.DetermineDirection relation))

[<Fact>]
let ``Relation of value prev should return Previous LinkDirection``() =
  let relation = "rel=\"prev\""

  Assert.Equal(LinkParser.LinkDirection.Previous, (LinkParser.DetermineDirection relation))

[<Fact>]
let ``Relation of value previous should return Previous LinkDirection``() =
  let relation = "rel=\"previous\""

  Assert.Equal(LinkParser.LinkDirection.Previous, (LinkParser.DetermineDirection relation))

[<Fact>]
let ``Relation of value junk should return Undetermined LinkDirection``() =
  let relation = "rel=\"junk\""

  Assert.Equal(LinkParser.LinkDirection.Undetermined, (LinkParser.DetermineDirection relation))

[<Fact>]
let ``Parse full Link with relation value of next should return Next as direction and url``() =
  let toParse = "<https://api.example.foobar.com/this/path/is/cool>; rel=\"next\""

  let expectedUrl = "https://api.example.foobar.com/this/path/is/cool"

  let result = LinkParser.Parse toParse
  let onlySomeResult = Array.choose id result
  let (url, direction) = Array.head onlySomeResult

  Assert.NotEmpty result
  Assert.NotEmpty onlySomeResult
  Assert.Equal(1, (Array.length onlySomeResult))
  Assert.Equal(expectedUrl, url)
  Assert.Equal(LinkParser.LinkDirection.Next, direction)


[<Fact>]
let ``Parse full Link with relation value of previous should return Previous as direction and url``() =
  let toParse = "<https://api.example.foobar.com/this/path/is/cool>; rel=\"previous\""

  let expectedUrl = "https://api.example.foobar.com/this/path/is/cool"

  let result = LinkParser.Parse toParse
  let onlySomeResult = Array.choose id result
  let (url, direction) = Array.head onlySomeResult

  Assert.NotEmpty result
  Assert.NotEmpty onlySomeResult
  Assert.Equal(1, (Array.length onlySomeResult))
  Assert.Equal(expectedUrl, url)
  Assert.Equal(LinkParser.LinkDirection.Previous, direction)


