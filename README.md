# The Algorithms - C#
[![Build Status](https://travis-ci.com/TheAlgorithms/C-Sharp.svg?branch=master)](https://travis-ci.com/TheAlgorithms/C-Sharp)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/6f6c4c370fc04857914dd04b91c5d675)](https://www.codacy.com/app/siriak/C-Sharp?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=TheAlgorithms/C-Sharp&amp;utm_campaign=Badge_Grade)
[![codecov](https://codecov.io/gh/TheAlgorithms/C-Sharp/branch/master/graph/badge.svg)](https://codecov.io/gh/TheAlgorithms/C-Sharp)
[![GuardRails badge](https://badges.guardrails.io/TheAlgorithms/C-Sharp.svg?token=84805208ba243f0931a74c5148883f894cbe9fd97fe54d64d6d0a89852067548)](https://dashboard.guardrails.io/default/gh/TheAlgorithms/C-Sharp)

This repository contains algorithms and data structures implemented in C# for eductional purposes.  

---

## Overview  
* [Algorithms](./Algorithms/)
	* [Data Compression](<./Algorithms/DataCompression>)
		* [Burrows-Wheeler transform](<./Algorithms/DataCompression/BurrowsWheelerTransform.cs>)
		* [Huffman Compressor](<./Algorithms/DataCompression/HuffmanCompressor.cs>)
		* [Shannon-Fano Compressor](<./Algorithms/DataCompression/ShannonFanoCompressor.cs>)
	* [Encoders](./Algorithms/Encoders/)
		* [Caesar](./Algorithms/Encoders/CaesarEncoder.cs)
		* [Vigenere](./Algorithms/Encoders/VigenereEncoder.cs)
		* [Hill](./Algorithms/Encoders/HillEncoder.cs)
		* [NYSIIS](./Algorithms/Encoders/NYSIISEncoder.cs)
		* [Soundex](./Algorithms/Encoders/SoundexEncoder.cs)
	* [Knapsack problem](./Algorithms/Knapsack/)
		* [Naive solver](./Algorithms/Knapsack/NaiveKnapsackSolver.cs)
		* [Dynamic Programming solver](./Algorithms/Knapsack/DynamicProgrammingKnapsackSolver.cs)
	* [Linear Algebra](./Algorithms/LinearAlgebra/)
		* [Eigenvalue](./Algorithms/LinearAlgebra/Eigenvalue/)
			* [Power Iteration](./Algorithms/LinearAlgebra/Eigenvalue/PowerIteration.cs)
	* [Numeric](./Algorithms/Numeric/)
		* [Decomposition](./Algorithms/Numeric/Decomposition)
			* [LU](./Algorithms/Numeric/Decomposition/LU.cs)
			* [Singular Vector Decomposition](./Algorithms/Numeric/Decomposition/SVD.cs)
		* [Greatest Common Divisor](./Algorithms/Numeric/GreatestCommonDivisor)
			* [Euclidean GCD](./Algorithms/Numeric/GreatestCommonDivisor/EuclideanGreatestCommonDivisorFinder.cs)
			* [Binary GCD](./Algorithms/Numeric/GreatestCommonDivisor/BinaryGreatestCommonDivisorFinder.cs)
		* [Factorization](./Algorithms/Numeric/Factorization)
			* [Trial division](./Algorithms/Numeric/Factorization/TrialDivisionFactorizer.cs)
		* [Series](./Algorithms/Numeric/Series/)
			* [Maclaurin](./Algorithms/Numeric/Series/Maclaurin.cs)
		* [Gauss-Jordan Elimination](./Algorithms/Numeric/GaussJordanElimination.cs)
		* [Pseudo-Inverse](./Algorithms/Numeric/Pseudoinverse/PseudoInverse.cs)
	* [Searches](./Algorithms/Search/)
		* [A-Star](./Algorithms/Search/AStar/)
		* [Binary](./Algorithms/Search/BinarySearcher.cs)
		* [Recursive Binary](./Algorithms/Search/RecursiveBinarySearcher.cs)
		* [Linear](./Algorithms/Search/LinearSearcher.cs)
		* [FastSearch](./Algorithms/Search/FastSearcher.cs)
		* [Knuth–Morris–Pratt](./Algorithms/Search/KmpSearcher.cs)
	* [Sorts](./Algorithms/Sorters/)
		* [Comparison](./Algorithms/Sorters/Comparison)
			* [Binary Insertion](./Algorithms/Sorters/Comparison/BinaryInsertionSorter.cs)
			* [Bogo](./Algorithms/Sorters/Comparison/BogoSorter.cs)
			* [Bubble](./Algorithms/Sorters/Comparison/BubbleSorter.cs)
			* [Cocktail](./Algorithms/Sorters/Comparison/CocktailSorter.cs)
			* [Comb](./Algorithms/Sorters/Comparison/CombSorter.cs)
			* [Cycle](./Algorithms/Sorters/Comparison/CycleSorter.cs)
			* [Heap](./Algorithms/Sorters/Comparison/HeapSorter.cs)
			* [Insertion](./Algorithms/Sorters/Comparison/InsertionSorter.cs)
			* [Merge](./Algorithms/Sorters/Comparison/MergeSorter.cs)
			* [Pancake](./Algorithms/Sorters/Comparison/PancakeSorter.cs)
			* [Quick](./Algorithms/Sorters/Comparison/QuickSorter.cs)
				* [Median of three pivot](./Algorithms/Sorters/Comparison/MedianOfThreeQuickSorter.cs)
				* [Middle point pivot](./Algorithms/Sorters/Comparison/MiddlePointQuickSorter.cs)
				* [Random pivot](./Algorithms/Sorters/Comparison/RandomPivotQuickSorter.cs)
			* [Selection](./Algorithms/Sorters/Comparison/SelectionSorter.cs)
			* [Shell](./Algorithms/Sorters/Comparison/ShellSorter.cs)
		* [External](./Algorithms/Sorters/External)
			* [Merge](./Algorithms/Sorters/External/ExternalMergeSorter.cs)
		* [Integer](./Algorithms/Sorters/Integer)
			* [Counting](./Algorithms/Sorters/Integer/CountingSorter.cs)
			* [Bucket](./Algorithms/Sorters/Integer/BucketSorter.cs)
			* [Radix](./Algorithms/Sorters/Integer/RadixSorter.cs)
		* [String](./Algorithms/Sorters/String)
			* [MSD Radix](./Algorithms/Sorters/String/MsdRadixStringSorter.cs)
	* [Sequences](./Algorithms/Sequences/)
		* [A000027 Natural](./Algorithms/Sequences/NaturalSequence.cs)
		* [A000040 Primes](./Algorithms/Sequences/PrimesSequence.cs)
		* [A000045 Fibonacci](./Algorithms/Sequences/FibonacciSequence.cs)
		* [A007318 Binomial](./Algorithms/Sequences/BinomialSequence.cs)
		* [A000142 Factorial](./Algorithms/Sequences/FactorialSequence.cs)
	* [String](./Algorithms/Strings/)
		* [Longest Consecutive Character](./Algorithms/Strings/GeneralStringAlgorithms.cs)
		* [Naive String Search](./Algorithms/Strings/NaiveStringSearch.cs)
		* [Rabin Karp](./Algorithms/Strings/RabinKarp.cs)
		* [Boyer Moore](./Algorithms/Strings/BoyerMoore.cs)
		* [Palindrome Checker](./Algorithms/Strings/palindrome.cs)
	* [Other](./Algorithms/Other/)
		* [Fermat Prime Checker](./Algorithms/Other/FermatPrimeChecker.cs)
		* [Sieve of Eratosthenes](./Algorithms/Other/SieveOfEratosthenes.cs)
		* [Luhn](./Algorithms/Other/Luhn.cs)
	* [Problems](./Algorithms/Problems/)
		* [Stable Marriage](./Algorithms/Problems/StableMarriage)
			* [Gale-Shapley](./Algorithms/Problems/StableMarriage/GaleShapley.cs)

* [Data Structures](./DataStructures/)
	* [Bit Array](./DataStructures/BitArray.cs)
	* [Singly Linked List](./DataStructures/SinglyLinkedList)
	* [Doubly Linked List](./DataStructures/DoublyLinkedList)
	* [Min-Max Heap](./DataStructures/MinMaxHeap.cs)
	* [Timeline](./DataStructures/Timeline.cs)
	* [Segment Trees](./DataStructures/SegmentTrees)
		* [Segment Tree](./DataStructures/SegmentTrees/SegmentTree.cs)
		* [Segment Tree Multiplication](./DataStructures/SegmentTrees/SegmentTreeApply.cs)
		* [Segment Tree Update](./DataStructures/SegmentTrees/SegmentTreeUpdate.cs)
	* [Array-based Queue](./DataStructures/ArrayBasedQueue.cs)
	* [List-based Queue](./DataStructures/ListBasedQueue.cs)
	* [Stack-based Queue](./DataStructures/StackBasedQueue.cs)
	* [Binary Search Tree](./DataStructures/BinarySearchTree)
	* [AA Tree](./DataStructures/AATree)
	* [Binary Heap](./DataStructures/BinaryHeap.cs)
---

## Contribution

You can contribute with pleasure to this repository. Please orient on the directory structure and overall code style of this repository. If you want to ask a question or suggest something, please open an issue.
