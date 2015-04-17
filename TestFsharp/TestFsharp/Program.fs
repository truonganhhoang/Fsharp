﻿// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module Test
let rec mem a l = match l with
  | [] -> false
  | hd::tl -> hd=a || mem a tl

let rec append l1 l2 = match l1 with
  | [] -> l2
  | hd :: tl -> hd :: append tl l2

let rec minus m n = m - n;;
                 
let rec max a b = if a > b then a else b

let rec getMaxList l = match l with
| [] -> failwith "Can't take the minimum of an empty list"
| [x] -> x
| x::xs ->
    let maxRest = getMaxList xs
    max x maxRest
       
//printfn "%A" (getMaxList [3;5;6;7;8]);;
//printfn "%A" (max 7 1);;

let code = []
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

let signChange (s:string) = [for c in s -> c]

let rec sign s = 
  match s with
  | [] -> [""]
  | x::xs -> if isSignal x then 
               x.ToString()::(sign xs)
              else (sign xs)

let rec add m n = (System.Int32.Parse(m.ToString()) + System.Int32.Parse(n.ToString())).ToString()
let rec summary s1 s2 = 
  match s1 with 
  | [] -> [""]
  | x::xs -> match s2 with 
                | [] ->  [""]
                | x2::xs2 -> if isSignal x && isSignal x2 then x.ToString()::(summary xs xs2)
                               else (add x x2)::(summary xs xs2)

let rec sum s1 s2 = if (sign s1) = (sign s2) then summary s1 s2
                     else ["Can not calculate sum of 2 string"]

let s1 = ['#';'9';'+';'1']
let s2 = ['#';'9';'+';'7']
let list = sum s1 s2
list |> List.iter (fun x -> printf "%s" x)

let pause () =  
  match System.Diagnostics.Debugger.IsAttached with  
  | true ->  
      printfn "\nPress any key to continue."  
      System.Console.ReadKey(true) |> ignore  
  | false -> ()

pause ()  