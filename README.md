** Solution that is able to extract the troubleshooting information from these manuals and others like them. **

Dear James,

I started looking at the task yesterday and found the challenge of extracting text from images in PDF documents quite intriguing. After experimenting with several OCR libraries, I settled on IronOcr instead of iText7, DocSumo, and others. Ultimately, I aimed to meet the requirements outlined in the task.

For the project, I opted to create a WebAPI with a single POST method for uploading PDFs. I have used Swagger to test the API and also included the PDFs you provided, along with few others, as models within the application itself.

Here's an overview of my progress on each task:

Task:
-----
1) Your task is to produce a solution that is able to extract the troubleshooting information from these manuals and others like them. **DONE**

       I managed to extract troubleshooting information from most manuals, but encountered significant
       challenges with the "Treadclimber manual.pdf" due to issues with the IronOcr tool.
       Despite trying various standard products available in the market, I couldn't achieve
       satisfactory results. However, with more time and effort, I believe there's potential
       for improvement.

3) Your solution should allow for a manual to be uploaded via an API endpoint. The troubleshooting information should be included in the response of this endpoint, or in the response of another endpoint
   depending on how you choose to implement your solution. **DONE**

       Implemented a functionality where manuals can be uploaded via an API endpoint. The troubleshooting
       information is included in the response of this endpoint, ensuring seamless access to relevant data.

5) The troubleshooting information that your API endpoint responds with should be json and should accurately represent the structure of the troubleshooting sections in the manuals. **DONE BUT NOT ACCURATELY**

      While I successfully extracted data, accurately representing the structure of troubleshooting sections in JSON proved challenging.
      The limited availability of free tools and the unordered nature of the extracted data posed difficulties in achieving perfect
      accuracy within the given time frame.

6) You should also specify the locations of each troubleshooting issue in the document, so that a front end application would be able to display citations of these sources  **DONE**
    
      The response JSON includes the page number, indicating the location of each troubleshooting issue in the document.
      This ensures that front-end applications can display citations of these sources effectively.

Overall, I believe the progress I've made thus far provides a solid foundation for demonstrating the task. I'm committed to continuing my efforts to refine and improve the solution further.
   
   
