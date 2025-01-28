# QU Developer Challenge: Word Finder

## Summary
This solution to the challenge is the result of trying three different methods of traversing a matrix. Each is separeted in its own class where the difference resides in the "findings of a word" and the common parts are all inside a base class. The main objective here is to showcase the time taken and the amount of memory consumed for each of them, being the Span solution the one that fits the best the objective of this challenge.

All of this was made as a TDD but only the best method of traversing the matrix is being used because, in the end, they all make the tests pass.

The premise of this is that the matrix could be a rectangular one and not just a square one, the code is prepeared for both of them.

## Usage
Using the Benchmark library, we are able to see the cost of each possible solution, performance and memory wise.

In release mode, manually run the performance test and, in order to see the results, go to the Output window and select Tests. Now you only need to wait for it to finish. Right at the end you can find a chart just like the one I´m showing here.

    Benchmark Results

    | Method                           | Mean         | Rank | Allocated | 
    |--------------------------------- |-------------:|------|-----------|
    | WordFinderBenchmarkSpanOnly      |     21.20 us |    1 | 	 2.3 KB  |
    | WordFinderBenchmarkRegexSolution |     86.47 us |    2 | 	 63.8 KB |
    | WordFinderBenchmarkIndexOf       | 57,271.57 us |    3 | 	 2.34 KB | 

## Contact
- camila.15694@gmail.com