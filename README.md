# TwitterTests
Selenium tests to test the Twitter Publish Feature (on the Web).
They include:
	- smoke test. It can be run after each code change to verify the basic functionality to publish a tweet with text content. 
	  Boundary value analysis technique was used to select input data.
	  
	- parameterized tests. They verify the capability to publish a tweet with image. 
	  Only allowed image formats JPG, PNG, GIF were selected based on the specification. 
	  Selected images have allowed image size of 1MB.

#How to Run Tests:

1. Check you have the following applications installed on the machine where the tests will run:
   - Visual Studio 2015
   - Chrome browser (latest version)
2. Download the repo from https://github.com/helen-gerasymenko/TwitterTests.git to some folder on your machine.
3. In the folder, double-click build.bat file to run tests.
4. See the test results in the open console window.


