using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace abcalbums.Controllers
{
    
   
        [Route("api/[controller]")]
        [ApiController]
        public class CupsController : ControllerBase
        {

            [HttpGet("swap")]
            public object Swap([FromBody] IEnumerable<string> SwapCommands)
            {
                // The ball starts in the position of 'B'
                var ballLocation = "B";

                // Read the list of swap commands

                // Start with the first command (loop until all swaps have been read/swaps have been made)
                //{
                foreach (string swap in SwapCommands)
                {
                    // Check if it is a valid swap
                    if (swap == "AB" || swap == "BA")
                    {
                        // If so, preform the swap
                        if (ballLocation == "A") { ballLocation = "B"; }
                        else if (ballLocation == "B") { ballLocation = "A"; }
                    }
                    else if (swap == "BC" || swap == "CB")
                    {
                        // If so, preform the swap
                        if (ballLocation == "B") { ballLocation = "C"; }
                        else if (ballLocation == "C") { ballLocation = "B"; }
                    }
                    else if (swap == "AC" || swap == "CA")
                    {
                        // If so, preform the swap
                        if (ballLocation == "A") { ballLocation = "C"; }
                        else if (ballLocation == "C") { ballLocation = "A"; }
                    }
                    else
                    {
                        // If it is not a valid swap return BadRequest message
                        //{

                        // If any swap contains more/less than two characters return a BadRequest with the appropriate message
                        if (swap.Length != 2) { return BadRequest("The swap command is not in the appropriate format of two characters (Swap command length error)"); }

                        // If any swap has lowercase letters then return a BadRequest with the appropriate message
                        // This needs to be placed before the Invalid char error, otherwise it won't be triggered (it is more specific than invalid char error)
                        foreach (char swapChar in swap)
                        {
                            if (Char.IsLower(swapChar)) { return BadRequest("There is a lowercase character in a swap command (Lowercase error)"); }
                        }

                        // If any swap contains characters that are not A, B or C return a BadRequest with the appropriate message
                        foreach (char swapChar in swap)
                        {
                            // Using the ASCII values of capital letters A, B and C (65, 66 and 67 respectively)
                            // to determine if any of the characters in the swap request are invalid
                            if ((int)swapChar < 65 || (int)swapChar > 67) { return BadRequest("There is an invalid character in the swap commands (Invalid char error)"); }
                        }

                        // If any swap is with itself (repeating cup name) return a BadRequest with the appropriate message
                        if (swap == "AA" || swap == "BB" || swap == "CC") { return BadRequest("The swap can't occur with only one cup (Identical cup error)"); }


                        // UNKNOWN ERROR message, in case there is a non-valid swap that the above don't catch (shouldn't be triggered unless the BadRequest checks miss something)
                        BadRequest("UNKNOWN ERROR -- Invalid swap request");

                        //} BadRequest checks/returns completed
                    }
                    //} Reading JSON string + Swapping cups completed

                    // For checking what got passed in, uncomment for use
                    //return SwapCommands;
                }
                return "The final position of the ball is: " + ballLocation;
            }
        }
    }

