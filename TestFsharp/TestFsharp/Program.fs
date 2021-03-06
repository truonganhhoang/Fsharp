﻿// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module Test

let s1 = ['#';'1';'2';'2';'+';'1';'-';'1';'1';'2']
let s2 = ['#';'1';'+';'1';'-';'1']
let s3 = ['#';'1';'-';'2']
let s4 = []
let s5 = ['#';'1';'#';'2';'-';'3';'#';'2']

//Check a in list l
let rec mem a l = 
  match l with
  | [] -> false
  | hd::tl -> hd=a || mem a tl

//Append two list
let rec append l1 l2 =
  match l1 with
  | [] -> l2
  | hd :: tl -> hd :: append tl l2

//Reverse two list
let rec reverse = function 
  | [] -> []
  | hd::tl -> append (reverse tl) [hd]

//Get max value in list
let rec getMaxList l = 
  match l with
  | [] -> failwith "Can't take the minimum of an empty list"
  | [x] -> x
  | x::xs ->
    let maxRest = getMaxList xs
    max x maxRest

/////// Common Function /////////
let string2char (s:string) = [for c in s -> c]
let rec charlist2stringlist (s:char list) = 
 match s with 
 | [] -> []
 | x::xs -> x.ToString()::charlist2stringlist xs
let rec stringToInt s = System.Int32.Parse(s |> String.concat "")
let rec charToInt s = System.Int32.Parse(charlist2stringlist(s) |> String.concat "")
let rec stringList2String s = s |> String.concat ""

let rec minus m n = m - n
let rec max (a:int) (b:int) = if a > b then a else b

///////End Common Function//////////
printfn "%A" (getMaxList [3;5;6;7;8]);;
printfn "%A" (max 7 1);;

let rec parse test = 
  match test with 
  | [] -> []
  | x::xs -> if x = "onacid" then 
                "+1"::(parse xs)
              else if x = "spawn" then 
               "(1"::(parse xs)
              else if x = "commit" then 
               "-1"::(parse xs)
              else if x = "endspawn" then 
               ")"::(parse xs)
              else (parse xs)

let isSignal s = if (s = '+' || s = '-' || s = '#' || s = ':') then true else false
let explode (s:string) = [for c in s -> c]
let string2 = explode "+1-1+1-1+1-1+1+1-1"

let rec balanceStr string2 = 
  match string2 with 
  | [] -> 0
  | x::xs -> if x = '+' then 
                1 + (balanceStr xs)
              else if x = '-' then 
               -1 + (balanceStr xs)
               else (balanceStr xs)
printfn "Test BalanceString %A" (balanceStr string2);;

let rec sign s = 
  match s with
  | [] -> [""]
  | x::xs -> if isSignal x then 
               x.ToString()::(sign xs)
              else (sign xs)

let isSharpNtemp s = 
  match s with
  | [] -> false
  | x::[] -> false
  | x::y::xs -> if (x = '#') && (y <> '#') then true
                  else false

let isSharpN s = if s = "" then false
                  else isSharpNtemp (string2char s)

let rec calWeightTemp (s:char list) (h1:int) = 
 match s with
 | [] -> [""]
 | x::tl -> if (h1 > 0) then calWeightTemp (tl) (h1 - 1) 
             else if (isSignal x) = false then x.ToString()::(calWeightTemp (tl) (h1 - 1))
               else [""]
let calWeight s h = (calWeightTemp s h) |> String.concat ""

printfn "Test calWeigh %s" (calWeight s1 1);;

let test = calWeight s1 2

let rec calSmallStemp (s:char list) (h:int) =
 match s with
 | [] -> [""]
 | x::tl -> if (h > 0) then calSmallStemp (tl) (h - 1) else x.ToString() :: if (isSignal x) = false then (calSmallStemp tl (h-1)) else [""]

let calSmallS (s:char list) (h:int) (ch:int) = 
    if (ch > 1) then reverse (calSmallStemp (reverse s) (s.Length - h)) |> String.concat "" else "#0"

printfn "Test calSmallS %s" (calSmallS s3 2 2);;

let rec getAfterClusterIndexTemp (s:char list) (h:int) (count:int) =
 match s with
 | [] -> -1
 | x::xs -> if (h >= 0) then getAfterClusterIndexTemp xs (h-1) count else if isSignal x then count else getAfterClusterIndexTemp xs (h-1) (count + 1)

let getAfterClusterIndex (s:char list) (h:int) = 
    if (getAfterClusterIndexTemp s h 0) > -1 then (getAfterClusterIndexTemp s h 0) else s.Length
    
let rec calBigsTemp (s:char list) (h:int) = 
 match s with 
 | [] -> [""]
 | x::xs -> if s.Length = getAfterClusterIndex s h then [""] else if (h> 0) then (x.ToString()::calBigsTemp xs (h-1)) else [""]

let calBigs (s:char list) (h:int) = calBigsTemp s h |> String.concat ""
printfn "Test calBig %s" (calBigs s3 2);;

let rec removeValue (s1:char list) =
 match s1 with
 | [] -> []
 | x::xs -> if (isSignal x) then x::xs else removeValue xs

let rec getValue (s1:char list) =
 match s1 with
 | [] -> []
 | x::xs -> if (isSignal x) then [] else x::getValue xs

//Join function
let rec change (h:int) = if (h> 0) then ":1"::change (h-1) else [""]

let rec joinTemp (s1:char list) (s2:char list) =
 match s1 with
 | [] -> change (charToInt s2)
 | x::xs -> if (isSignal x) then change (charToInt s2) else joinTemp xs (x::s2) 

let rec join (s1:char list) =
 match s1 with
 | [] -> [""]
 | x::xs -> if (isSignal x) then if x = '-' then append (joinTemp xs []) (join (removeValue xs))  else x.ToString()::join xs 
             else x.ToString()::join xs
printfn "Test Join %A" (join s5 |> String.concat "");;
//End Join function

//Start Merge function
let rec add (m:char list) (n:char list) = (charToInt(getValue(m)) + charToInt(getValue(n))).ToString()

let rec mergeTemp s1 s2 = 
  match s1 with 
  | [] -> [""]
  | x::xs -> match s2 with 
                | [] ->  [""]
                | x2 :: xs2 -> if isSignal x && isSignal x2 then x.ToString() :: (mergeTemp xs xs2)
                               else (add (x::xs) (x2::xs2))::(mergeTemp (removeValue(x::xs)) (removeValue(x2::xs2)))

let rec merge s1 s2 = if (sign s1) = (sign s2) then mergeTemp s1 s2
                       else ["Fail, can not merge 2 string"]
//End Merge function

let rec getNextSignal s = 
 match s with
 | [] -> ""
 | x::xs -> if isSignal x then x.ToString() else getNextSignal xs

let rec getNextValue s = 
 match s with
 | [] -> []
 | x::xs -> if isSignal x then getValue xs else getNextValue xs

let rec removeSignalValue s h = 
 match s with
 | [] -> []
 | x::xs -> if (isSignal x) && h > 0 then removeValue xs else removeSignalValue xs (h-1)

let s7 = ['#';'1';'#';'2';'-';'3';'+';'2';'+';'2';'2']
//Start Canonical function
let rec Canonical (s:char list) = 
 match s with
 | [] -> []
 | '#'::xs -> if (getNextSignal xs) = "#" then "#"+ (max (charToInt (getValue xs)) (charToInt(getNextValue xs))).ToString()::(Canonical(removeSignalValue xs 2)) else "#"::Canonical xs
 | '+'::xs -> if (getNextSignal xs) = "+" then "+"+ (add (getValue xs) (getNextValue xs)).ToString()::(Canonical(removeSignalValue xs 2)) else "+"::Canonical xs
 | '-'::xs -> if (getNextSignal xs) = "-" then "-"+ (add (getValue xs) (getNextValue xs)).ToString()::(Canonical(removeSignalValue xs 2)) else "-"::Canonical xs
 | x::xs -> x.ToString()::Canonical xs

printfn "\nTest Canonical %s" (stringList2String (Canonical s7));;
//End Canonical funtion

printfn "\nTest Merge %s" (stringList2String(merge s1 s2));;

let rec mergeOld (s1:char list) (s2:char list) = 
  match s1 with
  | [] -> match s2 with 
          | [] -> [""]
          | x::xs -> charlist2stringlist (x::xs)
  | x::xs -> match s2 with 
             | [] -> charlist2stringlist (x::xs)
             | hd::tl -> if (isSharpN (charlist2stringlist(x::xs) |> String.concat "")) && (isSharpN(charlist2stringlist(hd::tl) |> String.concat "")) 
                            then merge s1 s2 else charlist2stringlist (hd::tl)
//printfn "\nTest Merge %A" (mergeOld s4 s5);;

let pause () =  
  match System.Diagnostics.Debugger.IsAttached with  
  | true ->  
      printfn "\nPress any key to continue."  
      System.Console.ReadKey(true) |> ignore  
  | false -> ()

pause ()  