# Calculator by string expression - 2 hours

This solution is coded under the following rules:
- String expression will always only consist of numbers and operators, separated by spaces.

## Taking this string as a starting point 10 - ( 2 + 3 * ( 7 - 5 ) ).

1. Following the BODMAS/PEMDAS order, which is to evaluate the brackets first, the idea is to first calculate the innermost brackets.
2. From left to right, I'll first need to find the last occurence of the open bracket, and from that position, find the next closing bracket.
3. Using the substring method from the open bracket and the closing bracket positions, I'll be able to obtain the bracket string, i.e. ( 7 - 5 ).
4. Once I obtain the simplest form of the bracket, I can start applying the BODMAS rules to evaluate the expression's value.

## Evaluate ( 7 - 5 ).

1. Since all numbers and operators will be separated by space, I can split it by space and obtain an array of numbers and operators.
2. Since the bracket is not relevant here, the brackets can be trimmed off.
3. In this case, the array will be 

[0] = '7',
[1] = '-',
[2] = '5'

4. I'll need to create a function to evaluate the string going by BODMAS rules, so the first operator to consider is over/power-of.

## Evaluate over/power-of.

1. For example 2 ^ 3, I'll first find the position of ^.
2. With how the array is arranged, I'll take the position of the operator (x), as well as x-1 and x+1

[0] = '2',
[1] = '^',
[2] = '2'

3. Calculation goes by [operatorIndex - 1] ^ [operatorIndex + 1], with this, I can also construct the string that is intended to be replaced with.
4. One small issue that I encountered at this stage is that for power-of operator, it doesn't follow the left-to-right rule.
5. For example, 2 ^ 2 ^ 3, this should actually be evaluated as 2 ^ ( 2 ^ 3 ).
6. At this point, I did a minor adjustment on the position logic, to always take the last occurence of the operator instead.
7. Once I get the value of 2 ^ 3, I can replace the string with the calculated value, which brings me to 2 ^ 8.
8. The function will continue searching for the operator until the string no longer contains it.

## Evaluate multiplication/division

1. For example 2 / 2 * 5, since multiplication and division shares the same priority, I'll need to determine the first occurence of * and / to follow the left-to-right rule.
2. In this case, the first occurence is /.
3. I'll need to have a logic to determine what is the operator type for my calculation. This can be done by evaluating which of * and / comes first, and assigning the operator character to a variable.
4. Using a switch, I can determine what to do with the operator. If *, then multiply them, if /, then divide instead.
5. Similar to the previous approach, I'll need to construct the string to be replaced with.
6. In this case, the string to be replaced with is 2 / 2, with the value being 1. End result will be 1 * 5.
7. This will repeat until no more * or / is found.

## Evaluate addition/subtraction

1. For example 2 - 2 + 5, since addition and subtraction shares the same priority, I'll need to determine the first occurence of + and - to follow the left-to-right rule.
2. In this case, the first occurence is -.
3. I'll need to have a logic to determine what is the operator type for my calculation. This can be done by evaluating which of + and - comes first, and assigning the operator character to a variable.
4. Using a switch, I can determine what to do with the operator. If +, then add them, if -, then subtract instead.
5. Similar to the previous approach, I'll need to construct the string to be replaced with.
6. In this case, the string to be replaced with is 2 - 2, with the value being 0. End result will be 0 + 5.
7. This will repeat until no more + or - is found.

## Evaluate string expression: 10 - ( 2 + 3 * ( 7 - 5 ) ).

1. Using the concepts above, I'll evaluate the bracket first ( 7 - 5 ), which will then be updated to become 10 - ( 2 + 3 * 2 ).
2. Since the string still contains a bracket, evaluate the bracket again, which produce 10 - 8.
3. When there is no more brackets, evaluate the string as it is, which produces 2 in this case.
