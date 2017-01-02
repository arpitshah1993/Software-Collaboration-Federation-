﻿/////////////////////////////////////////////////////////////////////
// ITest.cs - Declare ITest interface method                       //
//                                                                 //
//                                                                 //
// Application: CSE681 - Software Modelling and Analysis,          //
// Test Harness Project-2                                          //
// Author:      Arpit Shah, Syracuse University,                   //
//              aushah@syr.edu, (646) 288-9410                     //
/////////////////////////////////////////////////////////////////////
/*
 * Module Operation:
 * ================
 * This module provides ITest interface to call test() method of each
 * test drivers. It wil provide interface so that we dont have to create
 * object of each test driver class.
 * 
 * Public Interface 
 * ================
 * interface ITest  //to provides interface to communicate test harness with test drivers. 
 * 
 * Build Process
 * =============
 * - Required Files: ITest.cs
 * - Compiler Command: csc ITest.cs
 * 
 * Maintainance History
 * ====================
 * ver 1.0 : 05 October 2016
 *     - first release 
 */
namespace Prototype1
{
    public interface ITest
    {
        bool test();
    }
}
