module LinkTests

open Xunit
open HttpHeader

[<Fact>]
let ``Relation of value next should return Next LinkDirection``() =
  let relation = "rel=\"next\""

  Assert.Equal(Link.Relation.Next, (Link.ParseRelation relation))

[<Fact>]
let ``Relation of value prev should return Previous LinkDirection``() =
  let relation = "rel=\"prev\""

  Assert.Equal(Link.Relation.Previous, (Link.ParseRelation relation))

[<Fact>]
let ``Relation of value previous should return Previous LinkDirection``() =
  let relation = "rel=\"previous\""

  Assert.Equal(Link.Relation.Previous, (Link.ParseRelation relation))

[<Fact>]
let ``Relation of value junk should return Undetermined LinkDirection``() =
  let relation = "rel=\"junk\""

  Assert.Equal(Link.Relation.Undetermined, (Link.ParseRelation relation))

[<Fact>]
let ``Parse full Link with relation value of next should return Next as direction and url``() =
  let toParse = "<https://api.example.foobar.com/this/path/is/cool>; rel=\"next\""

  let expectedUrl = "https://api.example.foobar.com/this/path/is/cool"

  let result = Link.Parse toParse
  let onlySomeResult = Array.choose id result
  let (url, direction) = Array.head onlySomeResult

  Assert.NotEmpty result
  Assert.NotEmpty onlySomeResult
  Assert.Equal(1, (Array.length onlySomeResult))
  Assert.Equal(expectedUrl, url)
  Assert.Equal(Link.Relation.Next, direction)

[<Fact>]
let ``Parse full Link with relation value of previous should return Previous as direction and url``() =
  let toParse = "<https://api.example.foobar.com/this/path/is/cool>; rel=\"previous\""

  let expectedUrl = "https://api.example.foobar.com/this/path/is/cool"

  let result = Link.Parse toParse
  let onlySomeResult = Array.choose id result
  let (url, direction) = Array.head onlySomeResult

  Assert.NotEmpty result
  Assert.NotEmpty onlySomeResult
  Assert.Equal(1, (Array.length onlySomeResult))
  Assert.Equal(expectedUrl, url)
  Assert.Equal(Link.Relation.Previous, direction)

[<Fact>]
let ``Parse full Link with both directions``() =
  let toParse = "<https://api.foobar.com/next_page>; rel=\"next\",<https://api.foobar.com/prev_page>; rel=\"prev\""

  let expectedNextPageUrl = "https://api.foobar.com/next_page"
  let expectedPreviousPageUrl = "https://api.foobar.com/prev_page"

  let result = Link.Parse toParse
  let onlySomeResult = Array.choose id result

  Assert.NotEmpty result
  Assert.NotEmpty onlySomeResult
  Assert.Equal(2, (Array.length onlySomeResult))

  let fstResult = Array.head onlySomeResult
  let sndResult = Array.last onlySomeResult

  let (nextUrl, nextDirection) = fstResult
  Assert.Equal(expectedNextPageUrl, nextUrl)
  Assert.Equal(Link.Relation.Next, nextDirection)

  let (prevUrl, prevDirection) = sndResult
  Assert.Equal(expectedPreviousPageUrl, prevUrl)
  Assert.Equal(Link.Relation.Previous, prevDirection)

