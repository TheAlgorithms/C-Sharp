using Algorithms.Graph.MinimumSpanningTree;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Algorithms.Tests.Graph.MinimumSpanningTree
{
    internal class KruskalTests
    {
        [Test]
        public void ValidateGraph_MatrixWrongSize_ThrowsException()
        {
            // Wrong number of columns
            var matrix = new[,]
            {
                { 0, 3, 4, float.PositiveInfinity },
                { 3, 0, 5, 6 },
                { 4, 5, 0, float.PositiveInfinity },
                { float.PositiveInfinity, 6, float.PositiveInfinity, 0 },
                { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity }
            };
            Assert.Throws<ArgumentException>(() => Kruskal.Solve(matrix));

            // Wrong number of rows
            matrix = new[,]
            {
                { 0, 3, 4, float.PositiveInfinity, float.PositiveInfinity },
                { 3, 0, 5, 6, 2 },
                { 4, 5, 0, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 6, float.PositiveInfinity, 0, float.PositiveInfinity }
            };
            Assert.Throws<ArgumentException>(() => Kruskal.Solve(matrix));
        }

        [Test]
        public void ValidateGraph_MatrixDirectedGraph_ThrowsException()
        {
            // Nodes 1 and 2 have a directed edge
            var matrix = new[,]
            {
                { 0, float.PositiveInfinity, 4, float.PositiveInfinity, float.PositiveInfinity },
                { 3, 0, 5, 6, 2 },
                { 4, 5, 0, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 6, float.PositiveInfinity, 0, float.PositiveInfinity },
                { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, 0 }
            };
            Assert.Throws<ArgumentException>(() => Kruskal.Solve(matrix));
        }

        [Test]
        public void Solve_MatrixGraph1_CorrectAnswer()
        {
            /* Graph
             *       (1)
             *       / \
             *      3   2
             *     /     \
             *   (0)--2--(2)
             */
            var matrix = new float[,]
            {
                { 0, 3, 2 },
                { 3, 0, 2 },
                { 2, 2, 0 }
            };

            /* Expected MST
             *      (1)
             *        \ 
             *         2
             *          \
             *  (0)--2--(2)
             */
            var expected = new[,]
            {
                { float.PositiveInfinity, float.PositiveInfinity, 2 },
                { float.PositiveInfinity, float.PositiveInfinity, 2 },
                { 2, 2, float.PositiveInfinity }
            };

            Kruskal.Solve(matrix).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
        }

        [Test]
        public void Solve_MatrixGraph2_CorrectAnswer()
        {
            /* Graph
             *  (0)     (4)
             *   |\     /
             *   | 3   2
             *   |  \ /
             *   4  (1)
             *   |  / \
             *   | 5   6
             *   |/     \
             *  (2)     (3)
             */
            var matrix = new[,]
            {
                { 0, 3, 4, float.PositiveInfinity, float.PositiveInfinity },
                { 3, 0, 5, 6, 2 },
                { 4, 5, 0, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 6, float.PositiveInfinity, 0, float.PositiveInfinity },
                { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, 0 }
            };

            /* Expected MST
             *  (0)     (4)
             *   |\     /
             *   | 3   2
             *   |  \ /
             *   4  (1)
             *   |    \
             *   |     6
             *   |      \
             *  (2)     (3)
             */
            var expected = new[,]
            {
                { float.PositiveInfinity, 3, 4, float.PositiveInfinity, float.PositiveInfinity },
                { 3, float.PositiveInfinity, float.PositiveInfinity, 6, 2 },
                { 4, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 6, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity }
            };

            Kruskal.Solve(matrix).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
        }

        [Test]
        public void Solve_MatrixGraph3_CorrectAnswer()
        {
            /* Graph
             *  (0)--3--(2)     (4)--2--(5)
             *    \     / \     /
             *     4   1   4   6
             *      \ /     \ /
             *      (1)--2--(3)
             */
            var matrix = new[,]
            {
                { 0, 4, 3, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { 4, 0, 1, 2, float.PositiveInfinity, float.PositiveInfinity },
                { 3, 1, 0, 4, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 2, 4, 0, 6, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, 0, 2 },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 2, 0 }
            };

            /* Graph
             *  (0)--3--(2)     (4)--2--(5)
             *          /       /
             *         1       6
             *        /       /
             *      (1)--2--(3)
             */
            var expected = new[,]
            {
                { float.PositiveInfinity, float.PositiveInfinity, 3, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, 1, 2, float.PositiveInfinity, float.PositiveInfinity },
                { 3, 1, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity, 2 },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 2, float.PositiveInfinity }
            };

            Kruskal.Solve(matrix).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
        }

        [Test]
        public void Solve_MatrixGraph4_CorrectAnswer()
        {
            /* Graph
             *  (0)--7--(1)--8--(2)
             *    \     / \     /
             *     5   9   7   5
             *      \ /     \ /
             *      (3)--15-(4)
             *        \     / \
             *         6   8   9
             *          \ /     \
             *          (5)--11-(6)
             */
            var matrix = new[,]
            {
                { 0, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { 7, 0, 8, 9, 7, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 8, 0, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity },
                { 5, 9, float.PositiveInfinity, 0, 15, 6, float.PositiveInfinity },
                { float.PositiveInfinity, 7, 5, 15, 0, 8, 9 },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, 8, 0, 11 },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 9, 11, 0 }
            };

            /* Expected MST
             *  (0)--7--(1)     (2)
             *    \       \     /
             *     5       7   5
             *      \       \ /
             *      (3)     (4)
             *        \       \
             *         6       9
             *          \       \
             *          (5)     (6)
             */
            var expected = new[,]
            {
                { float.PositiveInfinity, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { 7, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 7, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity },
                { 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity },
                { float.PositiveInfinity, 7, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 9 },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 9, float.PositiveInfinity, float.PositiveInfinity }
            };

            Kruskal.Solve(matrix).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
        }

        [Test]
        public void Solve_MatrixGraph5_CorrectAnswer()
        {
            /* Graph
             *  (0)--8--(1)--15-(2)
             *   |\     /     __/|\
             *   | 4   5  __25  13 12 
             *   |  \ /__/       |   \
             *  10  (3)----14---(4)  (5)
             *   |  / \        _/|   /
             *   | 9   6   __16  18 30
             *   |/     \ /      |/
             *  (6)--18-(7)--20-(8)
             */
            var matrix = new[,]
            {
                { 0, 8, float.PositiveInfinity, 4, float.PositiveInfinity, float.PositiveInfinity, 10, float.PositiveInfinity, float.PositiveInfinity },
                { 8, 0, 15, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, 15, 0, 25, 13, 12, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { 4, 5, 25, 0, 14, float.PositiveInfinity, 9, 6, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, 13, 14, 0, float.PositiveInfinity, float.PositiveInfinity, 16, 18 },
                { float.PositiveInfinity, float.PositiveInfinity, 12, float.PositiveInfinity, float.PositiveInfinity, 0, float.PositiveInfinity, float.PositiveInfinity, 30 },
                { 10, float.PositiveInfinity, float.PositiveInfinity, 9, float.PositiveInfinity, float.PositiveInfinity, 0, 18, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, 16, float.PositiveInfinity, 18, 0, 20 },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 18, 30, float.PositiveInfinity, 20, 0 }
            };

            /* Expected MST
             *  (0)     (1)     (2)
             *    \     /        |\
             *     4   5        13 12 
             *      \ /          |   \
             *      (3)----14---(4)  (5)
             *      / \          |
             *     9   6         18
             *    /     \        |
             *  (6)     (7)     (8)
             */
            var expected = new[,]
            {
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    4,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity
                },
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    5,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity
                },
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    13,
                    12,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity
                },
                {
                    4,
                    5,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    14,
                    float.PositiveInfinity,
                    9,
                    6,
                    float.PositiveInfinity
                },
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    13,
                    14,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    18
                },
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    12,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity
                },
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    9,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity
                },
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    6,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity
                },
                {
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    18,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity
                }
            };

            Kruskal.Solve(matrix).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
        }

        [Test]
        public void Solve_MatrixGraph6_CorrectAnswer()
        {
            /* Graph
             *  (0)--7--(1)     (2)
             *    \     /       /|
             *     5   9       5 |
             *      \ /       /  |
             *      (3)     (4)  2
             *              / \  |
             *             8   9 |
             *            /     \|
             *          (5)--11-(6)
             */
            var matrix = new[,]
            {
                { 0, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { 7, 0, float.PositiveInfinity, 9, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, 0, float.PositiveInfinity, 5, float.PositiveInfinity, 2 },
                { 5, 9, float.PositiveInfinity, 0, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, 0, 8, 9 },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 8, 0, 11 },
                { float.PositiveInfinity, float.PositiveInfinity, 2, float.PositiveInfinity, 9, 11, 0 }
            };

            /* Expected MST
             *  (0)--7--(1)     (2)
             *    \             /|
             *     5           5 |
             *      \         /  |
             *      (3)     (4)  2
             *              /    |
             *             8     |
             *            /      |
             *          (5)     (6)
             */
            var expected = new[,]
            {
                { float.PositiveInfinity, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { 7, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, 2 },
                { 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, 8, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 8, float.PositiveInfinity, float.PositiveInfinity },
                { float.PositiveInfinity, float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity }
            };

            Kruskal.Solve(matrix).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
        }
    }
}
