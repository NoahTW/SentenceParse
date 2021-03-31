/*
 * In the programming language of your choice, write a program that parses a
 * sentence and replaces each word with the following: first letter, number of 
 * distinct characters between first and last character, and last letter.
 * For example, Smooth would become S3h. Words are separated by spaces or
 * non-alphabetic characters and these separators should be maintained in their
 * original form and location in the answer. A few of the things we will be
 * looking at is accuracy, efficiency, solution completeness.
 * Please include this problem description in the comment
 * at the top of your solution.
*/


using System;

/**
* @author Noah Wardell
* Class Sentence Parse Method ParseString parses sentences to create new
* sentences with each word replaced with first letter, last letter and the
* number of distinct letters in-between, while preserving punctuation.
* Smooth becomes S3h, Wal-mart becomes W1l-m2t, Zoo! becomes Z1o!.
*/
class SentenceParse
{
   static void Main(string[] args)
   {
      string[] testSentences = { "I'm going to Wal-Mart, to do some shopping!",
      "This sentence has repeated letters in words like Smoothie & Mississippi.",
      "This sentence has no punctuation",
      "This. sentence. has. too. much. Punctuation!",
      "This sentence has dashes in the word Merry-go-round",
      "This senTence haS random CapitalizatioNs.",
      "A sentence that has a one letter word in it.",
      "This.Has.Lots-of?Symbols$inside-of*it"};

      //Test the ParseString method with various types of sentences.
      foreach (string sentence in testSentences)
      {
         Console.WriteLine($"Input sentence: {sentence} " +
                           $"\nResult sentence: {ParseString(sentence)}\n");
      }
   }

   /*Method ParseString creates a string with each word replaced with first
   * letter, LastLetter and the number of distinct letters in-between. 
   * Smooth returns S3h, etc.
   * @Param sentence the incoming string to be altered.
   * @Return the altered version of the sentence.
   */
   static string ParseString(string sentence)
   {
      //Characters used in the word so far
      string usedChars = "";

      //Split the sentence into an array of characters to be parsed
      char[] characters = sentence.ToCharArray();

      //New sentence to be returned
      string newSentence = "";

      //Running count of how many letters have been counted
      int letterCounter = 0;

      //Variable to hold the last letter read before a non-letter
      char lastLetter = ' ';//' ' = placeholder value

      //Boolean to keep track of whether we've counted the last letter
      bool lastLetterCounted = false;

      //Iterate through each letter, replace words with new format.
      for (int i = 0; i < characters.Length; i++)
      {
         char character = characters[i];
         //If the character is a letter, count it, and assign it
         //as the current lastLetter
         if (char.IsLetter(character))
         {
            //If this is the first letter of the word, add it to the newSentence
            if (letterCounter == 0)
            {
               newSentence += character;
            }

            //Lowercase string version of our character for comparison
            string charString = character.ToString().ToLower();

            //Check to see if we've counted this letter in this word already
            if (usedChars.Contains(charString))
            {
               lastLetterCounted = false;
            }
            else//if not, add it to the counter
            {
               lastLetterCounted = true;
               letterCounter++;
               //Add the character to the list of used characters
               usedChars += charString;
            }

            //Since the current character is a letter and not a symbol, update
            //the lastLetter to the current character
            lastLetter = character;

            //If the last character in the sentence is a letter, finish the
            //final word.
            if (i == characters.Length - 1)
            {
               FinishWord(ref newSentence, ref letterCounter, ref usedChars,
                          lastLetter, lastLetterCounted);
            }
         }

         //If the character is not a non-letter symbol, finish our new word
         //and reset the letter counter & chars counted
         else
         {
            //Finish the current word
            FinishWord(ref newSentence, ref letterCounter, ref usedChars,
                       lastLetter, lastLetterCounted);

            //add the non-letter character
            newSentence += character;
         }
      }
      return newSentence;
   }

   /* Method FinishWord finishes the current replacement word by adding the
   * count (if needed) and last letter (if needed) to the newSentence string,
   * then resets the letterCounter & usedChar values.
   * 
   * @Param newSentence The new Sentence the numbers/letters are being added to.
   * @Param letterCounter The number count of letters counted in the current word.
   * @Param usedChars The letters used in the current word.
   * @Param lastLetter The last letter in the current word.
   * @Param lastLetterCounted Whether or not the last letter was counted.
   */
   static void FinishWord(ref string newSentence, ref int letterCounter,
          ref string usedChars, char lastLetter, bool lastLetterCounted)
   {
      //If there are more than 2 letters, add the count of unique letters
      //to the word. This avoids words like "Go" becoming "G0o"
      if (letterCounter > 2)
      {
         newSentence += letterCounter - ((lastLetterCounted) ? 2 : 1);
      }

      //If there is more than one letter in the word, add the last letter
      //This avoids "I" from becoming "II"
      if (letterCounter > 1)
      {
         newSentence += lastLetter;
      }

      //Reset the letterCounter
      letterCounter = 0;
      //Reset the characters counted
      usedChars = "";
   }
}