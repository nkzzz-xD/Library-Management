Library app produced to specification
Utilises a dictionary of book objects with the book ID as the key. I decided to do this as it would allow for faster access times for 
books of a specific index(due to a hashing function being used). This allows the program to directly access books of an index to check 
if they are present in the library instead of looping through the whole library until the library of the required index is found 
(which would be the case had an array,arraylist,or similar been used). This is even more of a waste when the item is not found in the list
as the whole list would already have been traversed. The time complexity for the checking if an element is present is O(n) but I reduced it to O(1)
by using a dictionary.