<div align = "center">

# The Algorithms - C#

[![Discord chat](https://img.shields.io/discord/808045925556682782.svg?logo=discord&colorB=7289DA)](https://discord.gg/c7MnfGFGa6)
[![Build Status](https://app.travis-ci.com/TheAlgorithms/C-Sharp.svg?branch=master)](https://app.travis-ci.com/TheAlgorithms/C-Sharp)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/58895a2795bd48a8b3b7eb6ebe22d576)](https://www.codacy.com/gh/TheAlgorithms/C-Sharp/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=TheAlgorithms/C-Sharp&amp;utm_campaign=Badge_Grade)
[![codecov](https://codecov.io/gh/TheAlgorithms/C-Sharp/branch/master/graph/badge.svg)](https://codecov.io/gh/TheAlgorithms/C-Sharp)
[![GuardRails badge](https://badges.guardrails.io/TheAlgorithms/C-Sharp.svg?token=84805208ba243f0931a74c5148883f894cbe9fd97fe54d64d6d0a89852067548)](https://dashboard.guardrails.io/default/gh/TheAlgorithms/C-Sharp)
[![Donate](https://liberapay.com/assets/widgets/donate.svg)](https://liberapay.com/TheAlgorithms/donate)

## All Algorithms implemented in C# - for education purposes

The repository is a collection of a variety of algorithms implemented in C#. The algorithms span over a variety of topics
from computer science, mathematics and statistics, data science, machine learning, engineering, etc. The implementations
and their associated documentations are meant to provide a learning resource for educators and students. Hence, one may
find more than one implementation for the same objective but using different algorithm strategies and optimizations.

</div>

## List of Algorithms

* [Algorithms](./Algorithms)
  * [Data Compression](./Algorithms/DataCompression)
    * [Burrows-Wheeler transform](./Algorithms/DataCompression/BurrowsWheelerTransform.cs)
    * [Huffman Compressor](./Algorithms/DataCompression/HuffmanCompressor.cs)
    * [Shannon-Fano Compressor](./Algorithms/DataCompression/ShannonFanoCompressor.cs)
  * [Encoders](./Algorithms/Encoders)
    * [Caesar](./Algorithms/Encoders/CaesarEncoder.cs)
    * [Vigenere](./Algorithms/Encoders/VigenereEncoder.cs)
    * [Hill](./Algorithms/Encoders/HillEncoder.cs)
    * [NYSIIS](./Algorithms/Encoders/NysiisEncoder.cs)
    * [Soundex](./Algorithms/Encoders/SoundexEncoder.cs)
    * [Feistel](./Algorithms/Encoders/FeistelCipher.cs)
  * [Graph](./Algorithms/Graph)
    * [Minimum Spanning Tree](./Algorithms/Graph/MinimumSpanningTree)
      * [Prim's Algorithm (Adjacency Matrix)](./Algorithms/Graph/MinimumSpanningTree/PrimMatrix.cs)
      * [Kruskal's Algorithm](./Algorithms/Graph/MinimumSpanningTree/Kruskal.cs)
    * [BreadthFirstSearch](./Algorithms/Graph/BreadthFirstSearch.cs)
    * [DepthFirstSearch](./Algorithms/Graph/DepthFirstSearch.cs)
    * [Dijkstra Shortest Path](./Algorithms/Graph/Dijkstra/DijkstraAlgorithm.cs)
    * [FloydWarshall](./Algorithms/Graph/FloydWarshall.cs)
    * [Kosaraju](./Algorithms/Graph/Kosaraju.cs)
  * [Knapsack problem](./Algorithms/Knapsack)
    * [Naive solver](./Algorithms/Knapsack/NaiveKnapsackSolver.cs)
    * [Dynamic Programming solver](./Algorithms/Knapsack/DynamicProgrammingKnapsackSolver.cs)
    * [Branch and bound solver](./Algorithms/Knapsack/BranchAndBoundKnapsackSolver.cs)
    * [IHeuristicSolver](./Algorithms/Knapsack/IHeuristicSolver.cs)
  * [Linear Algebra](./Algorithms/LinearAlgebra)
    * [Distances](./Algorithms/LinearAlgebra/Distances)
      * [Euclidean](./Algorithms/LinearAlgebra/Distances/Euclidean.cs)
      * [Manhattan](./Algorithms/LinearAlgebra/Distances/Manhattan.cs)
    * [Eigenvalue](./Algorithms/LinearAlgebra/Eigenvalue)
      * [Power Iteration](./Algorithms/LinearAlgebra/Eigenvalue/PowerIteration.cs)
  * [Modular Arithmetic](./Algorithms/ModularArithmetic)
    * [Chinese Remainder Theorem](./Algorithms/ModularArithmetic/ChineseRemainderTheorem.cs)
    * [Extended Euclidean Algorithm](./Algorithms/ModularArithmetic/ExtendedEuclideanAlgorithm.cs)
    * [Modular Multiplicative Inverse](./Algorithms/ModularArithmetic/ModularMultiplicativeInverse.cs)
  * [Numeric](./Algorithms/Numeric)
    * [Aliquot Sum Calculator](./Algorithms/Numeric/AliquotSumCalculator.cs)
    * [Amicable Numbers Checker](./Algorithms/Numeric/AmicableNumbersChecker.cs)
    * [Decomposition](./Algorithms/Numeric/Decomposition)
      * [LU Decomposition](./Algorithms/Numeric/Decomposition/LU.cs)
      * [Thin Singular Vector Decomposition](./Algorithms/Numeric/Decomposition/ThinSVD.cs)
    * [Greatest Common Divisor](./Algorithms/Numeric/GreatestCommonDivisor)
      * [Euclidean GCD](./Algorithms/Numeric/GreatestCommonDivisor/EuclideanGreatestCommonDivisorFinder.cs)
      * [Binary GCD](./Algorithms/Numeric/GreatestCommonDivisor/BinaryGreatestCommonDivisorFinder.cs)
    * [Factorization](./Algorithms/Numeric/Factorization)
      * [Trial division Factorization](./Algorithms/Numeric/Factorization/TrialDivisionFactorizer.cs)
    * [Modular Exponentiation](./Algorithms/Numeric/ModularExponentiation.cs)
    * [Series](./Algorithms/Numeric/Series)
      * [Maclaurin Series](./Algorithms/Numeric/Series/Maclaurin.cs)
    * [Gauss-Jordan Elimination](./Algorithms/Numeric/GaussJordanElimination.cs)
    * [BinomialCoefficient](./Algorithms/Numeric/BinomialCoefficient.cs)
    * [Factorial](./Algorithms/Numeric/Factorial.cs)
    * [Keith Number Checker](./Algorithms/Numeric/KeithNumberChecker.cs)
    * [Pseudo-Inverse](./Algorithms/Numeric/Pseudoinverse/PseudoInverse.cs)
    * [Narcissistic Number Checker](./Algorithms/Numeric/NarcissisticNumberChecker.cs)
    * [Perfect Number Checker](./Algorithms/Numeric/PerfectNumberChecker.cs)
    * [Perfect Square Checker](./Algorithms/Numeric/PerfectSquareChecker.cs)
    * [Euler Method](./Algorithms/Numeric/EulerMethod.cs)
    * [Miller-Rabin primality check](./Algorithms/Numeric/MillerRabinPrimalityChecker.cs)
    * [KrishnamurthyNumberChecker](./Algorithms/Numeric/KrishnamurthyNumberChecker.cs)
    * [Automorphic Number](./Algorithms/Numeric/AutomorphicNumber.cs)
    * [Josephus Problem](./Algorithms/Numeric/JosephusProblem.cs)
  * [Searches](./Algorithms/Search)
    * [A-Star](./Algorithms/Search/AStar/)
    * [Binary Search](./Algorithms/Search/BinarySearcher.cs)
    * [Recursive Binary Search](./Algorithms/Search/RecursiveBinarySearcher.cs)
    * [Linear Search](./Algorithms/Search/LinearSearcher.cs)
    * [Fast Search](./Algorithms/Search/FastSearcher.cs)
    * [Fibonacci Search](./Algorithms/Search/FibonacciSearcher.cs)
    * [Jump Search](./Algorithms/Search/JumpSearcher.cs)
  * [Sorts](./Algorithms/Sorters)
    * [Comparison](./Algorithms/Sorters/Comparison)
      * [Binary Insertion Sort](./Algorithms/Sorters/Comparison/BinaryInsertionSorter.cs)
      * [Bogo Sort](./Algorithms/Sorters/Comparison/BogoSorter.cs)
      * [Bubble Sort](./Algorithms/Sorters/Comparison/BubbleSorter.cs)
      * [Cocktail Sort](./Algorithms/Sorters/Comparison/CocktailSorter.cs)
      * [Comb Sort](./Algorithms/Sorters/Comparison/CombSorter.cs)
      * [Cycle Sort](./Algorithms/Sorters/Comparison/CycleSorter.cs)
      * [Exchange Sort](./Algorithms/Sorters/Comparison/ExchangeSorter.cs)
      * [Heap Sort](./Algorithms/Sorters/Comparison/HeapSorter.cs)
      * [Insertion Sort](./Algorithms/Sorters/Comparison/InsertionSorter.cs)
      * [Merge Sort](./Algorithms/Sorters/Comparison/MergeSorter.cs)
      * [Pancake Sort](./Algorithms/Sorters/Comparison/PancakeSorter.cs)
      * [Quick Sort](./Algorithms/Sorters/Comparison/QuickSorter.cs)
        * [Median of three pivot](./Algorithms/Sorters/Comparison/MedianOfThreeQuickSorter.cs)
        * [Middle point pivot](./Algorithms/Sorters/Comparison/MiddlePointQuickSorter.cs)
        * [Random pivot](./Algorithms/Sorters/Comparison/RandomPivotQuickSorter.cs)
      * [Selection Sort](./Algorithms/Sorters/Comparison/SelectionSorter.cs)
      * [Shell Sort](./Algorithms/Sorters/Comparison/ShellSorter.cs)
      * [Tim Sort](./Algorithms/Sorters/Comparison/TimSorter.cs)
    * [External](./Algorithms/Sorters/External)
      * [Merge Sort](./Algorithms/Sorters/External/ExternalMergeSorter.cs)
    * [Integer](./Algorithms/Sorters/Integer)
      * [Counting Sort](./Algorithms/Sorters/Integer/CountingSorter.cs)
      * [Bucket Sort](./Algorithms/Sorters/Integer/BucketSorter.cs)
      * [Radix Sort](./Algorithms/Sorters/Integer/RadixSorter.cs)
    * [String](./Algorithms/Sorters/String)
      * [MSD Radix Sort](./Algorithms/Sorters/String/MsdRadixStringSorter.cs)
      * [Knuth–Morris–Pratt Search](./Algorithms/Strings/KnuthMorrisPrattSearcher.cs)
  * [Shufflers](./Algorithms/Shufflers)
    * [Fisher-Yates Shuffler](./Algorithms/Shufflers/FisherYatesShuffler.cs)
  * [Sequences](./Algorithms/Sequences)
    * [A000002 Kolakoski](./Algorithms/Sequences/KolakoskiSequence.cs)
    * [A000004 Zero](./Algorithms/Sequences/ZeroSequence.cs)
    * [A000005 Count of Divisors](./Algorithms/Sequences/DivisorsCountSequence.cs)
    * [A000008 Make Change](./Algorithms/Sequences/MakeChangeSequence.cs)
    * [A000010 Euler's Totient](./Algorithms/Sequences/EulerTotientSequence.cs)
    * [A000012 All Ones](./Algorithms/Sequences/AllOnesSequence.cs)
    * [A000027 Natural](./Algorithms/Sequences/NaturalSequence.cs)
    * [A000032 Lucas Numbers](./Algorithms/Sequences/LucasNumbersBeginningAt2Sequence.cs)
    * [A000040 Primes](./Algorithms/Sequences/PrimesSequence.cs)
    * [A000045 Fibonacci](./Algorithms/Sequences/FibonacciSequence.cs)
    * [A000079 Powers of 2](./Algorithms/Sequences/PowersOf2Sequence.cs)
    * [A000108 Catalan](./Algorithms/Sequences/CatalanSequence.cs)
    * [A000120 1's Counting](./Algorithms/Sequences/OnesCountingSequence.cs)
    * [A000142 Factorial](./Algorithms/Sequences/FactorialSequence.cs)
    * [A000215 Fermat Numbers](./Algorithms/Sequences/FermatNumbersSequence.cs)
    * [A000290 Squares](./Algorithms/Sequences/SquaresSequence.cs)
    * [A000578 Cubes](./Algorithms/Sequences/CubesSequence.cs)
    * [A000720 PrimePi](./Algorithms/Sequences/PrimePiSequence.cs)
    * [A001146 Number of Boolean Functions](./Algorithms/Sequences/NumberOfBooleanFunctionsSequence.cs)
    * [A001462 Golomb's](./Algorithms/Sequences/GolombsSequence.cs)
    * [A001478 Negative Integers](./Algorithms/Sequences/NegativeIntegersSequence.cs)
    * [A002110 Primorial Numbers](./Algorithms/Sequences/PrimorialNumbersSequence.cs)
    * [A005132 Recaman's](./Algorithms/Sequences/RecamansSequence.cs)
    * [A006577 Number of '3n+1' steps to reach 1](./Algorithms/Sequences/ThreeNPlusOneStepsSequence.cs)
    * [A006862 Euclid Numbers](./Algorithms/Sequences/EuclidNumbersSequence.cs)
    * [A006879 Number of Primes by Number of Digits](./Algorithms/Sequences/NumberOfPrimesByNumberOfDigitsSequence.cs)
    * [A006880 Number of Primes by Powers of 10](./Algorithms/Sequences/NumberOfPrimesByPowersOf10Sequence.cs)
    * [A007318 Binomial](./Algorithms/Sequences/BinomialSequence.cs)
    * [A007395 All Twos](./Algorithms/Sequences/AllTwosSequence.cs)
    * [A010051 Binary Prime Constant](./Algorithms/Sequences/BinaryPrimeConstantSequence.cs)
    * [A010701 All Threes](./Algorithms/Sequences/BinaryPrimeConstantSequence.cs)
    * [A011557 Powers of 10](./Algorithms/Sequences/PowersOf10Sequence.cs)
    * [A057588 Kummer Numbers](./Algorithms/Sequences/KummerNumbersSequence.cs)
    * [A019434 Fermat Primes](./Algorithms/Sequences/FermatPrimesSequence.cs)
    * [A181391 Van Eck's](./Algorithms/Sequences/VanEcksSequence.cs)
  * [String](./Algorithms/Strings)
    * [Longest Consecutive Character](./Algorithms/Strings/GeneralStringAlgorithms.cs)
    * [Naive String Search](./Algorithms/Strings/NaiveStringSearch.cs)
    * [Rabin Karp](./Algorithms/Strings/RabinKarp.cs)
    * [Boyer Moore](./Algorithms/Strings/BoyerMoore.cs)
    * [Palindrome Checker](./Algorithms/Strings/Palindrome.cs)
    * [Get all permutations of a string](./Algorithms/Strings/Permutation.cs)
    * [Levenshtein Distance](./Algorithms/Strings/LevenshteinDistance.cs)
    * [Hamming Distance](./Algorithms/Strings/HammingDistance.cs)
    * [Jaro Similarity](./Algorithms/Strings/JaroSimilarity.cs)
    * [Jaro-Winkler Distance](./Algorithms/Strings/JaroWinklerDistance.cs)
  * [Other](./Algorithms/Other)
    * [Fermat Prime Checker](./Algorithms/Other/FermatPrimeChecker.cs)
    * [Sieve of Eratosthenes](./Algorithms/Other/SieveOfEratosthenes.cs)
    * [Luhn](./Algorithms/Other/Luhn.cs)
    * [Int2Binary](./Algorithms/Other/Int2Binary.cs)
    * [GeoLocation](./Algorithms/Other/GeoLocation.cs)
    * [Mandelbrot](./Algorithms/Other/Mandelbrot.cs)
    * [Koch Snowflake](./Algorithms/Other/KochSnowflake.cs)
    * [RGB-HSV Conversion](./Algorithms/Other/RGBHSVConversion.cs)
    * [Flood Fill](./Algorithms/Other/FloodFill.cs)
    * [Pareto Optimization](./Algorithms/Other/ParetoOptimization.cs)
    * [Gauss Optimization](./Algorithms/Other/GaussOptimization.cs)
    * [Decisions Convolutions](./Algorithms/Other/DecisionsConvolutions.cs)
  * [Problems](./Algorithms/Problems)
    * [Stable Marriage](./Algorithms/Problems/StableMarriage)
      * [Gale-Shapley](./Algorithms/Problems/StableMarriage/GaleShapley.cs)
      * [Accepter](./Algorithms/Problems/StableMarriage/Accepter.cs)
      * [Proposer](./Algorithms/Problems/StableMarriage/Proposer.cs)
    * [N-Queens](./Algorithms/Problems/NQueens)
      * [Backtracking](./Algorithms/Problems/NQueens/BacktrackingNQueensSolver.cs)
    * [Dynamic Coin Change](./Algorithms/Problems/CoinChange)
      * [Dynamic](./Algorithms/Problems/CoinChange/DynamicCoinChangeSolver.cs)

* [Data Structures](./DataStructures)
  * [Bit Array](./DataStructures/BitArray.cs)
  * [Timeline](./DataStructures/Timeline.cs)
  * [Segment Trees](./DataStructures/SegmentTrees)
    * [Segment Tree](./DataStructures/SegmentTrees/SegmentTree.cs)
    * [Segment Tree Multiplication](./DataStructures/SegmentTrees/SegmentTreeApply.cs)
    * [Segment Tree Update](./DataStructures/SegmentTrees/SegmentTreeUpdate.cs)
  * [Binary Search Tree](./DataStructures/BinarySearchTree)
  * [Scapegoat Tree](./DataStructures/ScapegoatTree)
  * [Fenwick tree (or Binary Indexed Tree)](./DataStructures/BinaryIndexedTree)
  * [AA Tree](./DataStructures/AATree)
  * [AVL Tree](./DataStructures/AVLTree)
  * [Red-Black Tree](./DataStructures/RedBlackTree)
  * [Stack](./DataStructures/Stack)
    * [Array-based Stack](./DataStructures/Stack/ArrayBasedStack.cs)
    * [List-based Stack](./DataStructures/Stack/ListBasedStack.cs)
  * [Heap](./DataStructures/Heap)
    * [Min-Max Heap](./DataStructures/Heap/MinMaxHeap.cs)
    * [Binary Heap](./DataStructures/Heap/BinaryHeap.cs)
    * [Fibonacci Heap](./DataStructures/Heap/FibonacciHeap/FibonacciHeap.cs)
    * [Pairing Heap](./DataStructures/Heap/PairingHeap/PairingHeap.cs)
  * [Probabilistic](./DataStructures/Probabilistic)
    * [BloomFilter](./DataStructures/Probabilistic/BloomFilter.cs)
    * [Count-Min Sketch](./DataStructures/Probabilistic/CountMinSketch.cs)
    * [HyperLogLog](./DataStructures/Probabilistic/HyperLogLog.cs)
  * [Queue](./DataStructures/Queue)
    * [Array-based Queue](./DataStructures/Queue/ArrayBasedQueue.cs)
    * [List-based Queue](./DataStructures/Queue/ListBasedQueue.cs)
    * [Stack-based Queue](./DataStructures/Queue/StackBasedQueue.cs)
  * [Linked List](./DataStructures/LinkedList)
    * [Singly Linked List](./DataStructures/LinkedList/SinglyLinkedList/SinglyLinkedList.cs)
    * [Doubly Linked List](./DataStructures/LinkedList/DoublyLinkedList/DoublyLinkedList.cs)
    * [Skip List](./DataStructures/LinkedList/SkipList/SkipList.cs)
  * [Graph](./DataStructures/Graph)
    * [Directed Weighted Graph Via Adjacency Matrix](./DataStructures/Graph/DirectedWeightedGraph.cs)
  * [Disjoint Set](./DataStructures/DisjointSet)
  * [SortedList](./DataStructures/SortedList.cs)
  * [Inverted index](./DataStructures/InvertedIndex.cs)
  * [Unrolled linked list](./DataStructures/UnrolledList/UnrolledLinkedList.cs)
  * [Tries](./DataStructures/Tries/Trie.cs)
  * [Cache](./DataStructures/Cache)
    * [Least Frequently Used (LFU) Cache](./DataStructures/Cache/LfuCache.cs)
    * [Least Recently Used (LRU) Cache](./DataStructures/Cache/LruCache.cs)


## Contributing

You can contribute with pleasure to this repository.
Please orient on the directory structure and overall code style of this repository
and refer to [our contributing guidelines](./CONTRIBUTING.md) for more details.
If you want to ask a question or suggest something, please open an issue.
