# The Algorithms - C#



All Algorithms implemented in C# - for education purposes

所有算法均以 C# 实现 - 用于教育目的

The repository is a collection of a variety of algorithms implemented in C#. The algorithms span over a variety of topics from computer science, mathematics and statistics, data science, machine learning, engineering, etc. The implementations
and their associated documentations are meant to provide a learning resource for educators and students. Hence, one may find more than one implementation for the same objective but using different algorithm strategies and optimizations.

这个知识库是在C #中实现的多种算法的集合。这些算法涵盖了计算机科学、数学与统计学、数据科学、机器学习、工程学等多个领域。这些实现及其相关的文档旨在为教育者和学生提供一个学习资源。因此，对于同一个目标，可以使用不同的算法策略和优化方法，找到多个实现。



# List of Algorithms | 算法大全目录

# [Algorithms | 算法](./Algorithms)

## [Crypto | 加密](./Algorithms/Crypto/)

### [Paddings | 填充方式](./Algorithms/Crypto/Paddings/)
#### [ISO 10125-2 Padding | ISO 10125-2 填充](./Algorithms/Crypto/Paddings/Iso10126D2Padding.cs)

#### [ISO 7816-4 Padding | ISO 7816-4 填充](./Algorithms/Crypto/Paddings/Iso7816D4Padding.cs)

#### [X9.32 Padding | X9.32 填充](./Algorithms/Crypto/Paddings/X932Padding.cs)
#### [TBC Padding | TBC 填充](./Algorithms/Crypto/Paddings/TbcPadding.cs)

#### [PKCS7 Padding | PKCS7 填充](./Algorithms/Crypto/Paddings/Pkcs7Padding.cs)
### [Digests | 摘要算法](./Algorithms/Crypto/Digests/)
##### [MD2 Digest | MD2 摘要](./Algorithms/Crypto/Digests/Md2Digest.cs)
## [Data Compression | 数据压缩](./Algorithms/DataCompression)
### [Burrows-Wheeler transform | Burrows-Wheeler 变换](./Algorithms/DataCompression/BurrowsWheelerTransform.cs)
### [Huffman Compressor | 霍夫曼压缩](./Algorithms/DataCompression/HuffmanCompressor.cs)
### [Shannon-Fano Compressor | 香农-费诺压缩](./Algorithms/DataCompression/ShannonFanoCompressor.cs)
## [Encoders | 编码器](./Algorithms/Encoders)
### [Caesar | 凯撒加密](./Algorithms/Encoders/CaesarEncoder.cs)
### [Vigenere | 维吉尼亚加密](./Algorithms/Encoders/VigenereEncoder.cs)
### [Hill | 希尔加密](./Algorithms/Encoders/HillEncoder.cs)
### [NYSIIS | NYSIIS 编码](./Algorithms/Encoders/NysiisEncoder.cs)
### [Soundex | Soundex 编码](./Algorithms/Encoders/SoundexEncoder.cs)
### [Feistel | Feistel 加密](./Algorithms/Encoders/FeistelCipher.cs)
### [Blowfish | Blowfish 加密](./Algorithms/Encoders/BlowfishEncoder.cs)
## [Graph | 图算法](./Algorithms/Graph)
### [Minimum Spanning Tree | 最小生成树](./Algorithms/Graph/MinimumSpanningTree)
#### [Prim's Algorithm (Adjacency Matrix) | 普里姆算法（邻接矩阵）](./Algorithms/Graph/MinimumSpanningTree/PrimMatrix.cs)
#### [Kruskal's Algorithm | 克鲁斯卡尔算法](./Algorithms/Graph/MinimumSpanningTree/Kruskal.cs)
### [BreadthFirstTreeTraversal | 广度优先树遍历](./Algorithms/Graph/BreadthFirstTreeTraversal.cs)
### [BreadthFirstSearch | 广度优先搜索](./Algorithms/Graph/BreadthFirstSearch.cs)
### [DepthFirstSearch | 深度优先搜索](./Algorithms/Graph/DepthFirstSearch.cs)
### [Dijkstra Shortest Path | 迪杰斯特拉最短路径算法](./Algorithms/Graph/Dijkstra/DijkstraAlgorithm.cs)
### [FloydWarshall | 弗洛伊德-沃肖尔算法](./Algorithms/Graph/FloydWarshall.cs)
### [Kosaraju | Kosaraju 算法](./Algorithms/Graph/Kosaraju.cs)
## [Knapsack problem | 背包问题](./Algorithms/Knapsack)
### [Naive solver | 朴素求解器](./Algorithms/Knapsack/NaiveKnapsackSolver.cs)
### [Dynamic Programming solver | 动态规划求解器](./Algorithms/Knapsack/DynamicProgrammingKnapsackSolver.cs)
### [Branch and bound solver | 分支限界求解器](./Algorithms/Knapsack/BranchAndBoundKnapsackSolver.cs)
### [IHeuristicKnapsackSolver | 启发式背包求解器](./Algorithms/Knapsack/IHeuristicKnapsackSolver.cs)
## [Linear Algebra | 线性代数](./Algorithms/LinearAlgebra)
### [Distances | 距离计算](./Algorithms/LinearAlgebra/Distances)
#### [Euclidean | 欧几里得距离](./Algorithms/LinearAlgebra/Distances/Euclidean.cs)
#### [Manhattan | 曼哈顿距离](./Algorithms/LinearAlgebra/Distances/Manhattan.cs)
### [Eigenvalue | 特征值](./Algorithms/LinearAlgebra/Eigenvalue)
#### [Power Iteration | 幂迭代](./Algorithms/LinearAlgebra/Eigenvalue/PowerIteration.cs)
## [Modular Arithmetic | 模运算](./Algorithms/ModularArithmetic)
### [Chinese Remainder Theorem | 中国剩余定理](./Algorithms/ModularArithmetic/ChineseRemainderTheorem.cs)
### [Extended Euclidean Algorithm | 拓展欧几里得算法](./Algorithms/ModularArithmetic/ExtendedEuclideanAlgorithm.cs)
### [Modular Multiplicative Inverse | 模逆元](./Algorithms/ModularArithmetic/ModularMultiplicativeInverse.cs)
## [Numeric | 数值算法](./Algorithms/Numeric)
### [Absolute | 绝对值](./Algorithms/Numeric/Abs.cs) 
### [Aliquot Sum Calculator | 过量和计算器](./Algorithms/Numeric/AliquotSumCalculator.cs)
### [Amicable Numbers Checker | 亲和数检查器](./Algorithms/Numeric/AmicableNumbersChecker.cs)
### [Decomposition | 分解](./Algorithms/Numeric/Decomposition)
#### [LU Decomposition | LU 分解](./Algorithms/Numeric/Decomposition/LU.cs)
#### [Thin Singular Vector Decomposition | 精简奇异值分解](./Algorithms/Numeric/Decomposition/ThinSVD.cs)
### [Greatest Common Divisor | 最大公约数](./Algorithms/Numeric/GreatestCommonDivisor)
#### [Euclidean GCD | 欧几里得最大公约数](./Algorithms/Numeric/GreatestCommonDivisor/EuclideanGreatestCommonDivisorFinder.cs)
#### [Binary GCD | 二进制最大公约数](./Algorithms/Numeric/GreatestCommonDivisor/BinaryGreatestCommonDivisorFinder.cs)
### [Factorization | 因式分解](./Algorithms/Numeric/Factorization)
#### [Trial division Factorization | 试除法分解](./Algorithms/Numeric/Factorization/TrialDivisionFactorizer.cs)
### [Modular Exponentiation | 模幂运算](./Algorithms/Numeric/ModularExponentiation.cs)
### [Series | 数列](./Algorithms/Numeric/Series)
#### [Maclaurin Series | 麦克劳林级数](./Algorithms/Numeric/Series/Maclaurin.cs)
### [Gauss-Jordan Elimination | 高斯-约旦消元法](./Algorithms/Numeric/GaussJordanElimination.cs)
### [BinomialCoefficient | 二项系数](./Algorithms/Numeric/BinomialCoefficient.cs)
### [Factorial | 阶乘](./Algorithms/Numeric/Factorial.cs)
### [Keith Number Checker | 凯斯数检查器](./Algorithms/Numeric/KeithNumberChecker.cs)
### [Pseudo-Inverse | 伪逆矩阵](./Algorithms/Numeric/Pseudoinverse/PseudoInverse.cs)
### [Narcissistic Number Checker | 自恋数检查器](./Algorithms/Numeric/NarcissisticNumberChecker.cs)
### [Perfect Number Checker | 完美数检查器](./Algorithms/Numeric/PerfectNumberChecker.cs)
### [Perfect Square Checker | 完全平方数检查器](./Algorithms/Numeric/PerfectSquareChecker.cs)
### [Euler Method | 欧拉法](./Algorithms/Numeric/EulerMethod.cs)
### [Classic Runge-Kutta Method | 经典龙格-库塔法](./Algorithms/Numeric/RungeKuttaMethod.cs)
### [Miller-Rabin primality check | 米勒-拉宾素性测试](./Algorithms/Numeric/MillerRabinPrimalityChecker.cs)
### [KrishnamurthyNumberChecker | 克里希纳穆尔提数检查器](./Algorithms/Numeric/KrishnamurthyNumberChecker.cs)
### [Automorphic Number | 同构数](./Algorithms/Numeric/AutomorphicNumber.cs)
### [Josephus Problem | 约瑟夫问题](./Algorithms/Numeric/JosephusProblem.cs)
### [Newton's Square Root Calculation | 牛顿平方根计算](./Algorithms/NewtonSquareRoot.cs)
### [SoftMax Function | SoftMax 函数](./Algorithms/Numeric/SoftMax.cs)
## [Searches | 搜索算法](./Algorithms/Search)
### [A-Star | A* 算法](./Algorithms/Search/AStar/)
### [Binary Search | 二分查找](./Algorithms/Search/BinarySearcher.cs)
### [BoyerMoore Search | 博耶尔-穆尔查找](./Algorithms/Search/BoyerMoore.cs)
### [Fast Search | 快速查找](./Algorithms/Search/FastSearcher.cs)
### [Fibonacci Search | 斐波那契查找](./Algorithms/Search/FibonacciSearcher.cs)
### [Interpolation Search | 插值查找](./Algorithms/Search/InterpolationSearch.cs)
### [Jump Search | 跳跃查找](./Algorithms/Search/JumpSearcher.cs)
### [Linear Search | 线性查找](./Algorithms/Search/LinearSearcher.cs)
### [Recursive Binary Search | 递归二分查找](./Algorithms/Search/RecursiveBinarySearcher.cs)  
### [Sorts | 排序算法](./Algorithms/Sorters)
#### [Comparison | 比较排序](./Algorithms/Sorters/Comparison)
##### [Binary Insertion Sort | 二分插入排序](./Algorithms/Sorters/Comparison/BinaryInsertionSorter.cs)
##### [Bogo Sort | 博戈排序](./Algorithms/Sorters/Comparison/BogoSorter.cs)
##### [Bubble Sort | 冒泡排序](./Algorithms/Sorters/Comparison/BubbleSorter.cs)
##### [Cocktail Sort | 鸡尾酒排序](./Algorithms/Sorters/Comparison/CocktailSorter.cs)
##### [Comb Sort | 梳排序](./Algorithms/Sorters/Comparison/CombSorter.cs)
##### [Cycle Sort | 循环排序](./Algorithms/Sorters/Comparison/CycleSorter.cs)
##### [Exchange Sort | 交换排序](./Algorithms/Sorters/Comparison/ExchangeSorter.cs)
##### [Heap Sort | 堆排序](./Algorithms/Sorters/Comparison/HeapSorter.cs)
##### [Insertion Sort | 插入排序](./Algorithms/Sorters/Comparison/InsertionSorter.cs)
##### [Merge Sort | 归并排序](./Algorithms/Sorters/Comparison/MergeSorter.cs)
##### [Pancake Sort | 煎饼排序](./Algorithms/Sorters/Comparison/PancakeSorter.cs)
##### [Quick Sort | 快速排序](./Algorithms/Sorters/Comparison/QuickSorter.cs)
###### [Median of three pivot | 三点中值快排](./Algorithms/Sorters/Comparison/MedianOfThreeQuickSorter.cs)
###### [Middle point pivot | 中间点快排](./Algorithms/Sorters/Comparison/MiddlePointQuickSorter.cs)
###### [Random pivot | 随机快排](./Algorithms/Sorters/Comparison/RandomPivotQuickSorter.cs)
##### [Selection Sort | 选择排序](./Algorithms/Sorters/Comparison/SelectionSorter.cs)
##### [Shell Sort | 希尔排序](./Algorithms/Sorters/Comparison/ShellSorter.cs)
##### [Tim Sort | Tim排序](./Algorithms/Sorters/Comparison/TimSorter.cs)
##### [Simplified Tim Sort | 简化Tim排序](./Algorithms/Sorters/Comparison/BasicTimSorter.cs)
#### [External | 外部排序](./Algorithms/Sorters/External)
##### [Merge Sort | 外部归并排序](./Algorithms/Sorters/External/ExternalMergeSorter.cs)
#### [Integer | 整数排序](./Algorithms/Sorters/Integer)
##### [Counting Sort | 计数排序](./Algorithms/Sorters/Integer/CountingSorter.cs)
##### [Bucket Sort | 桶排序](./Algorithms/Sorters/Integer/BucketSorter.cs)
##### [Radix Sort | 基数排序](./Algorithms/Sorters/Integer/RadixSorter.cs)
#### [String | 字符串排序](./Algorithms/Sorters/String)
##### [MSD Radix Sort | MSD基数排序](./Algorithms/Sorters/String/MsdRadixStringSorter.cs)

### [Shufflers | 洗牌算法](./Algorithms/Shufflers)
#### [Fisher-Yates Shuffler | Fisher-Yates洗牌](./Algorithms/Shufflers/FisherYatesShuffler.cs)

### [Sequences | 数列](./Algorithms/Sequences)
#### [A000002 Kolakoski | Kolakoski序列](./Algorithms/Sequences/KolakoskiSequence.cs)
#### [A000004 Zero | 零序列](./Algorithms/Sequences/ZeroSequence.cs)
#### [A000005 Count of Divisors | 因子计数](./Algorithms/Sequences/DivisorsCountSequence.cs)
#### [A000008 Make Change | 找零序列](./Algorithms/Sequences/MakeChangeSequence.cs)
#### [A000010 Euler's Totient | 欧拉函数](./Algorithms/Sequences/EulerTotientSequence.cs)
#### [A000012 All Ones | 全1序列](./Algorithms/Sequences/AllOnesSequence.cs)
#### [A000027 Natural | 自然数序列](./Algorithms/Sequences/NaturalSequence.cs)
#### [A000032 Lucas Numbers | Lucas数列](./Algorithms/Sequences/LucasNumbersBeginningAt2Sequence.cs)
#### [A000040 Primes | 素数序列](./Algorithms/Sequences/PrimesSequence.cs)
#### [A000045 Fibonacci | 斐波那契数列](./Algorithms/Sequences/FibonacciSequence.cs)
#### [A000079 Powers of 2 | 2的幂](./Algorithms/Sequences/PowersOf2Sequence.cs)
#### [A000108 Catalan | 卡塔兰数](./Algorithms/Sequences/CatalanSequence.cs)
#### [A000120 1's Counting | 1的计数](./Algorithms/Sequences/OnesCountingSequence.cs)
#### [A000124 Central Polygonal Numbers | 中心多边形数](./Algorithms/Sequences/CentralPolygonalNumbersSequence.cs)
#### [A000125 Cake Numbers | 蛋糕数](./Algorithms/Sequences/CakeNumbersSequence.cs)
#### [A000142 Factorial | 阶乘数列](./Algorithms/Sequences/FactorialSequence.cs)
#### [A000213 Tribonacci Numbers | 三项式数列](./Algorithms/Sequences/TribonacciNumbersSequence.cs)
#### [A000215 Fermat Numbers | 费马数](./Algorithms/Sequences/FermatNumbersSequence.cs)
#### [A000288 Tetranacci Numbers | 四项式数列](./Algorithms/Sequences/TetranacciNumbersSequence.cs)
#### [A000290 Squares | 平方数](./Algorithms/Sequences/SquaresSequence.cs)
#### [A000292 Tetrahedral numbers | 四面体数](./Algorithms/Sequences/TetrahedralSequence.cs)
#### [A000578 Cubes | 立方数](./Algorithms/Sequences/CubesSequence.cs)
#### [A000720 PrimePi | 素数个数函数](./Algorithms/Sequences/PrimePiSequence.cs)
#### [A001146 Number of Boolean Functions | 布尔函数个数](./Algorithms/Sequences/NumberOfBooleanFunctionsSequence.cs)
#### [A001462 Golomb's | Golomb序列](./Algorithms/Sequences/GolombsSequence.cs)
#### [A001478 Negative Integers | 负整数序列](./Algorithms/Sequences/NegativeIntegersSequence.cs)
#### [A002110 Primorial Numbers | 素数阶乘数](./Algorithms/Sequences/PrimorialNumbersSequence.cs)
#### [A002717 Matchstick Triangle Arrangement | 火柴三角排列数](./Algorithms/Sequences/MatchstickTriangleSequence.cs)
#### [A005132 Recaman's | Recaman序列](./Algorithms/Sequences/RecamansSequence.cs)
#### [A006577 Number of '3n+1' steps to reach 1 | 3n+1步数](./Algorithms/Sequences/ThreeNPlusOneStepsSequence.cs)
#### [A006862 Euclid Numbers | 欧几里得数](./Algorithms/Sequences/EuclidNumbersSequence.cs)
#### [A006879 Number of Primes by Number of Digits | 素数按位数计数](./Algorithms/Sequences/NumberOfPrimesByNumberOfDigitsSequence.cs)
#### [A006880 Number of Primes by Powers of 10 | 素数按10的幂计数](./Algorithms/Sequences/NumberOfPrimesByPowersOf10Sequence.cs)
#### [A007318 Binomial | 二项式数列](./Algorithms/Sequences/BinomialSequence.cs)
#### [A007395 All Twos | 全2序列](./Algorithms/Sequences/AllTwosSequence.cs)
#### [A010051 Binary Prime Constant | 二进制素数常数](./Algorithms/Sequences/BinaryPrimeConstantSequence.cs)
#### [A010701 All Threes | 全3序列](./Algorithms/Sequences/AllTwosSequence.cs)
#### [A011557 Powers of 10 | 10的幂](./Algorithms/Sequences/PowersOf10Sequence.cs)
#### [A057588 Kummer Numbers | 库默数](./Algorithms/Sequences/KummerNumbersSequence.cs)
#### [A019434 Fermat Primes | 费马素数](./Algorithms/Sequences/FermatPrimesSequence.cs)
#### [A181391 Van Eck's | Van Eck序列](./Algorithms/Sequences/VanEcksSequence.cs)

### [String | 字符串算法](./Algorithms/Strings)
#### [Similarity | 相似度](./Algorithms/Strings/Similarity/)

## [String | 字符串](./Algorithms/Strings)
### [Similarity | 相似度](./Algorithms/Strings/Similarity/)
#### [Cosine Similarity | 余弦相似度](./Algorithms/Strings/Similarity/CosineSimilarity.cs)
#### [Damerau-Levenshtein Distance | 达梅劳-莱文斯坦距离](./Algorithms/Strings/Similarity/DamerauLevenshteinDistance.cs)
#### [Hamming Distance | 汉明距离](./Algorithms/Strings/Similarity/HammingDistance.cs)
#### [Jaro Similarity | 贾罗相似度](./Algorithms/Strings/Similarity/JaroSimilarity.cs)
#### [Jaro-Winkler Distance | 贾罗-温克勒距离](./Algorithms/Strings/Similarity/JaroWinklerDistance.cs)
#### [Optimal String Alignment | 最优字符串对齐](./Algorithms/Strings/Similarity/OptimalStringAlignment.cs)
### [Pattern Matching | 模式匹配](./Algorithms/Strings/PatternMatching/)
#### [Bitop Pattern Matching | 位操作模式匹配](./Algorithms/Strings/PatternMatching/Bitap.cs)
#### [Naive String Search | 朴素字符串搜索](./Algorithms/Strings/PatternMatching/NaiveStringSearch.cs)
#### [Rabin Karp | 拉宾-卡普算法](./Algorithms/Strings/PatternMatching/RabinKarp.cs)
#### [Boyer Moore | 博耶-摩尔算法](./Algorithms/Strings/PatternMatching/BoyerMoore.cs)
#### [Knuth–Morris–Pratt Search | 克努斯-莫里斯-普拉特搜索](./Algorithms/Strings/PatternMatching/KnuthMorrisPrattSearcher.cs)
#### [WildCard Pattern Matching | 通配符模式匹配](./Algorithms/Strings/PatternMatching/WildCardMatcher.cs)
#### [Z-block substring search | Z块子字符串搜索](./Algorithms/Strings/PatternMatching/ZblockSubstringSearch.cs)
### [Longest Consecutive Character | 最长连续字符](./Algorithms/Strings/GeneralStringAlgorithms.cs)
### [Palindrome Checker | 回文检查器](./Algorithms/Strings/Palindrome.cs)
### [Get all permutations of a string | 获取字符串的所有排列](./Algorithms/Strings/Permutation.cs)
## [Other | 其他](./Algorithms/Other)
### [Fermat Prime Checker | 费马素数检查器](./Algorithms/Other/FermatPrimeChecker.cs)
### [Sieve of Eratosthenes | 埃拉托斯特尼筛法](./Algorithms/Other/SieveOfEratosthenes.cs)
### [Luhn | 露恩算法](./Algorithms/Other/Luhn.cs)
### [Int2Binary | 整数转二进制](./Algorithms/Other/Int2Binary.cs)
### [GeoLocation | 地理位置](./Algorithms/Other/GeoLocation.cs)
### [Mandelbrot | 曼德尔布罗特](./Algorithms/Other/Mandelbrot.cs)
### [Koch Snowflake | 科赫雪花曲线](./Algorithms/Other/KochSnowflake.cs)
### [RGB-HSV Conversion | RGB-HSV转换](./Algorithms/Other/RGBHSVConversion.cs)
### [Flood Fill | 区域填充](./Algorithms/Other/FloodFill.cs)
### [Pareto Optimization | 帕累托优化](./Algorithms/Other/ParetoOptimization.cs)
### [Gauss Optimization | 高斯优化](./Algorithms/Other/GaussOptimization.cs)
### [Decisions Convolutions | 决策卷积](./Algorithms/Other/DecisionsConvolutions.cs)
### [Welford's Variance | 韦尔福德方差](./Algorithms/Other/WelfordsVariance.cs)
### [Julian Easter | 儒略复活节](./Algorithms/Other/JulianEaster.cs)
### [Pollard's Rho | 波拉德的 rho 算法](./Algorithms/Other/PollardsRhoFactorizing.cs)
## [Problems | 问题](./Algorithms/Problems)
### [Stable Marriage | 稳定婚姻](./Algorithms/Problems/StableMarriage)
#### [Gale-Shapley | 加尔-沙普利算法](./Algorithms/Problems/StableMarriage/GaleShapley.cs)
#### [Accepter | 接受者](./Algorithms/Problems/StableMarriage/Accepter.cs)
#### [Proposer | 提议者](./Algorithms/Problems/StableMarriage/Proposer.cs)
### [N-Queens | N皇后](./Algorithms/Problems/NQueens)
#### [Backtracking | 回溯算法](./Algorithms/Problems/NQueens/BacktrackingNQueensSolver.cs)
### [Dynamic Programming | 动态规划](./Algorithms/Problems/DynamicProgramming)
#### [Coin Change | 硬币找零](./Algorithms/Problems/DynamicProgramming/CoinChange/DynamicCoinChangeSolver.cs)
#### [Levenshtein Distance | 莱文斯坦距离](./Algorithms/Problems/DynamicProgramming/LevenshteinDistance/LevenshteinDistance.cs)

# [Data Structures | 数据结构](./DataStructures)
## [Bit Array | 位数组](./DataStructures/BitArray.cs)
## [Timeline | 时间线](./DataStructures/Timeline.cs)
## [Segment Trees | 线段树](./DataStructures/SegmentTrees)
### [Segment Tree | 线段树](./DataStructures/SegmentTrees/SegmentTree.cs)
### [Segment Tree Multiplication | 线段树乘法](./DataStructures/SegmentTrees/SegmentTreeApply.cs)
### [Segment Tree Update | 线段树更新](./DataStructures/SegmentTrees/SegmentTreeUpdate.cs)
## [Binary Search Tree | 二叉搜索树](./DataStructures/BinarySearchTree)
## [Scapegoat Tree | 替罪羊树](./DataStructures/ScapegoatTree)
## [Fenwick tree (or Binary Indexed Tree) | 费诺树（或二叉索引树）](./DataStructures/Fenwick/BinaryIndexedTree.cs)
## [AA Tree | AA树](./DataStructures/AATree)
## [AVL Tree | AVL树](./DataStructures/AVLTree)
## [Red-Black Tree | 红黑树](./DataStructures/RedBlackTree)
## [Stack | 栈](./DataStructures/Stack)
### [Array-based Stack | 基于数组的栈](./DataStructures/Stack/ArrayBasedStack.cs)
### [List-based Stack | 基于链表的栈](./DataStructures/Stack/ListBasedStack.cs)
### [Queue-based Stack | 基于队列的栈](./DataStructures/Stack/QueueBasedStack.cs)
## [Heap | 堆](./DataStructures/Heap)
### [Min-Max Heap | 最小-最大堆](./DataStructures/Heap/MinMaxHeap.cs)
### [Binary Heap | 二叉堆](./DataStructures/Heap/BinaryHeap.cs)
### [Fibonacci Heap | 斐波那契堆](./DataStructures/Heap/FibonacciHeap/FibonacciHeap.cs)
### [Pairing Heap | 配对堆](./DataStructures/Heap/PairingHeap/PairingHeap.cs)
## [Probabilistic | 概率性数据结构](./DataStructures/Probabilistic)
### [BloomFilter | 布隆过滤器](./DataStructures/Probabilistic/BloomFilter.cs)
### [Count-Min Sketch | 计数-最小草图](./DataStructures/Probabilistic/CountMinSketch.cs)
### [HyperLogLog | 超级日志日志](./DataStructures/Probabilistic/HyperLogLog.cs)
## [Queue | 队列](./DataStructures/Queue)
### [Array-based Queue | 基于数组的队列](./DataStructures/Queue/ArrayBasedQueue.cs)
### [List-based Queue | 基于链表的队列](./DataStructures/Queue/ListBasedQueue.cs)
### [Stack-based Queue | 基于栈的队列](./DataStructures/Queue/StackBasedQueue.cs)
## [Linked List | 链表](./DataStructures/LinkedList)
### [Singly Linked List | 单链表](./DataStructures/LinkedList/SinglyLinkedList/SinglyLinkedList.cs)
### [Doubly Linked List | 双链表](./DataStructures/LinkedList/DoublyLinkedList/DoublyLinkedList.cs)
### [Skip List | 跳表](./DataStructures/LinkedList/SkipList/SkipList.cs)
## [Graph | 图](./DataStructures/Graph)
# [Directed Weighted Graph Via Adjacency Matrix | 通过邻接矩阵的有向加权图](./DataStructures/Graph/DirectedWeightedGraph.cs)
## [Disjoint Set | 不相交集合](./DataStructures/DisjointSet)
## [SortedList | 排序列表](./DataStructures/SortedList.cs)
## [Inverted index | 反向索引](./DataStructures/InvertedIndex.cs)
## [Unrolled linked list | 展开链表](./DataStructures/UnrolledList/UnrolledLinkedList.cs)
## [Tries | 字典树](./DataStructures/Tries/Trie.cs)
## [HashTable | 哈希表](./DataStructures/Hashing/HashTable.cs)
## [Cache | 缓存](./DataStructures/Cache)
### [Least Frequently Used (LFU) Cache | 最不经常使用（LFU）缓存](./DataStructures/Cache/LfuCache.cs)
### [Least Recently Used (LRU) Cache | 最近最少使用（LRU）缓存](./DataStructures/Cache/LruCache.cs)



Project Update: .NET 8 Migration

As part of our continuous effort to stay up-to-date with the latest technologies, we have migrated our project to .NET 8. This upgrade enhances our project with the latest features and improvements from the .NET ecosystem.

作为与最新技术保持同步的持续努力的一部分，我们已经将我们的项目迁移到了。net 8。这次升级用。net生态系统的最新特性和改进增强了我们的项目。



New Requirements

新需求



To build and run this project, .NET 8 SDK  is now required.

为构建和运行本项目，现在需要Net 8Sdk。



Ensure your development tools are compatible with .NET 8.

确保你的开发工具与。net 8兼容。



Building the Project

With .NET 8 SDK installed, you can build the project using the standard `dotnet build` command.

安装了。net 8 SDK后，您可以使用标准的“dotnet build”命令构建项目。



All existing build scripts have been updated to accommodate the .NET 8 SDK.

所有现有的构建脚本都已更新以适应。net 8 SDK。



Our comprehensive suite of unit tests ensures compatibility with .NET 8.

我们全面的单元测试套件确保了与。net 8的兼容性。



Run tests using the `dotnet test` command as usual.

像往常一样使用' dotnet test '命令运行测试。



Contributing

You can contribute with pleasure to this repository.
Please orient on the directory structure and overall code style of this repository
and refer to [our contributing guidelines](./CONTRIBUTING.md) for more details.
If you want to ask a question or suggest something, please open an issue.

您可以愉快地为这个存储库做出贡献。请参考此存储库的目录结构和整体代码风格，并参考[我们的贡献指南](./ contributing.com .md)了解更多细节。如果您想提问或提出建议，请打开issue。