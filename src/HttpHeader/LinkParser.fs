namespace HttpHeader

open System
open System.Text.RegularExpressions

module LinkParser =
    let private _splitPattern = ",\s*<"
    let private _relationMatchPattern = "\s*(.+)\s*=\s*\"?([^\"]+)\"?"

    type LinkDirection = Next | Previous | Undetermined

    let (|MatchNext|_|) (str : String) =
        match (str.ToLower()) with
        | "next"  -> Some Next
        | _       -> None

    let (|MatchPrevious|_|) (str : String) =
        match (str.ToLower()) with
        | "prev" | "previous" -> Some Previous
        | _                   -> None

    let SplitContainsTwoItems array = Array.length array = 2

    let SplitUrlAndRelation part = Regex.Split(part, ";")

    let DetermineDirection relation =
        let _match = Regex.Match(relation, _relationMatchPattern)
        let relationValue = _match.Groups.[2].Value

        match relationValue with
        | MatchNext     direction -> direction
        | MatchPrevious direction -> direction
        | _                       -> Undetermined

    let Parse value =
        Regex.Split(value, _splitPattern)
        |> Array.map (fun part ->
            let split = SplitUrlAndRelation part

            if SplitContainsTwoItems split then
                let dirtyUrl = split |> Array.head
                let relation = split |> Array.last

                let sanitizedUrl = Regex.Replace(dirtyUrl, "[<>]", "")
                let direction = DetermineDirection relation

                Some (sanitizedUrl, direction)
            else
                None
        )