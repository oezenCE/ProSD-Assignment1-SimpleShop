using System;

namespace CodeSnippets{
    
    public class CodeSnippets{

        /// <summary>
        /// function1 investigates whether the given string is symmetrical or not. 
        /// Basically, it rewrites the input string into a char array, then reverse it
        /// to backside-front. In implementational point of view, it compares the lower-lettered
        /// samples to bypass the lack of case-sensitive investigation.
        /// 
        /// Rating 1
        /// </summary>
        /// <returns>bool</returns>
        static bool function1(string pattern) {
            var parts = pattern.ToCharArray();
            Array.Reverse(parts);
            var starp = (new string(parts)).ToLower();
            
            var b = pattern.ToLower().Equals(starp);
            return b;
        }


        /// <summary>
        /// Sorting algorithm that is similar to insertion sort, but in piece-wise intervals. 
        /// Basically, instead of sorting in large intervals, it chooses a reduced interval, 
        /// namely here length/2, sorts each those individual pairs, then reduces the interval
        /// in the next iteration, and sorts those new pairs. This continues until the interval
        /// is reduced to 1. In short, shifting the elements in piece-wise intervals, 
        /// which is reduced every iteration, provides a pre-sorted structure when the interval 
        /// is reduced to the lowest, which is one. In worst case, the last iteration requires
        /// shifting every element with their pairs, which yields the same time complexity of standard insertion sort.
        /// 
        /// insertion sort    : O(n^2) = function2 in worst case. 
        /// function2 sorting : (maybe) <O(n^2) in average.
        /// 
        /// 
        /// ### Potential Future Reference For Myself ###
        /// Edit after a bit research: It seems the below implementation of function2, which was named as "ShellSort" has the same complexity
        /// with standard insertion sort, O(n^2). The complexity of this algorithm is entirely depends on the gap size and reducing pattern.
        /// Archive source for the gap size selection : "https://en.wikipedia.org/wiki/Shellsort#Gap_sequences"
        /// Keywords: Knuth, Sedgewick
        /// 
        /// Rating 3
        /// </summary>
        /// <returns>function return signature is int and it will be return 0, independent of operation. It is basically a fake void function</returns>
        /// <returns>int</returns>
        public static int function2(int[] numbers){

            
            // First iteration (gap size length/2)............................: To be sorted : arr[0]<->arr[length/2], arr[1]<->arr[length/2+1], ..., arr[length/2-1]<->arr[length-1]
            // Second iteration (gap size length/4)...........................: To be sorted : arr[0]<->arr[length/4], arr[1]<->arr[length/4+1], ..., arr[3*(length/4)-1]<->arr[length-1]
            // ... 
            // Last iteration (gap size length = 1, regular insertion sort)...: To be sorted : arr[0]<->arr[1], arr[1]<->arr[2], ..., arr[length-2]<->arr[length-1]
            for (var h = numbers.Length / 2; h > 0; h /= 2){ 
                for (var i = h; i < numbers.Length; i += 1){ 
                    var temp = numbers[i];                    
                                                             


                    int t;
                    for (t = i; t >= h && numbers[t - h] > temp; t -= h){ 
                                                                          
                                                                          
                        numbers[t] = numbers[t - h];
                    }
                    numbers[t] = temp;
                }
            }
            return 0;
        }
    }
}

