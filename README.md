# Derivative Calculator (C# Windows Forms)

A symbolic differentiation calculator built using **C#** and **Windows Forms** that parses mathematical expressions, constructs expression trees, computes derivatives symbolically, and simplifies results into standard polynomial form.

This project demonstrates compiler-style parsing, object-oriented design, and symbolic mathematics implemented from scratch without external math libraries.

---

## Overview

This application allows users to input algebraic expressions and compute their derivatives symbolically rather than numerically.  

Instead of evaluating functions at points, the program:

1. Tokenizes user input
2. Parses expressions into an expression tree
3. Applies differentiation rules recursively
4. Simplifies and expands the resulting expression

The system functions similarly to a lightweight computer algebra system.

---

## Features

- Symbolic differentiation
- Expression tree architecture
- Recursive descent parser
- Polynomial simplification
- Automatic expansion of products
- Combination of like terms
- Canonical polynomial ordering
- Implicit multiplication support (e.g., `4x`, `(x+1)(x-1)`)
- Quotient rule support

### Example Expression Tree

##3x^2 + 4x

           Add
          /   \
      Product  Product
      /     \   /    \
  3       Power  4     x
           / \
          x   2
---


## Supported Operations

- Addition and subtraction  
- Multiplication  
- Division  
- Exponentiation (`x^n`)
- Parenthesized expressions  

### Example Inputs
x^3 + 2x
x / 2x + 1
2 * x * x
### Example Output
Input: (3x^2)(4x+3)
Derivative: 36x^2 + 18x


---

## Work in Progress
This project is currently under active development. Features may change, and the project is not yet ready for production use. Please feel free to leave any feedback or suggestions of new features. I would eventually like to add functionality for integrals.
