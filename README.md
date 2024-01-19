<h1 align="center">Working with Legislative Data</h1>
<p align="center">
    A worker to analyze legislation data
</p>
  
## Index

1. [About Application](#about-application)
2. [Questions](#questions)
3. [How to run](#run)

## About Application

This application is part of a test, where an analysis of legislative data must be conducted, and files with the results should be generated.

### Questions

- Discuss your solution’s time complexity. What tradeoffs did you make?

I opted for the creation of a worker that will generate the results when all date files are present; therefore, the complexity became O(n²) due to the main loop and the grouping and counting operations.

- How would you change your solution to account for future columns that might be requested, such as “Bill Voted On Date” or “Co-Sponsors”?

 I would need to map these new columns to the domain for reading files with the new columns and modify the algorithm to take these pieces of information into account in the analysis.

- How would you change your solution if instead of receiving CSVs of data, you were given a list of legislators or bills that you should generate a CSV for?

I didn't fully understand the question, but if instead of receiving this initial data through CSV files, perhaps you receive it after consuming an endpoint (is that correct?), with these data lists. First, I would generate files with the consumed data, save them within the "Data" folder, and then proceed with the main analysis.

- How long did you spend working on the assignment?

I spent about 5 hours, divided among the planned days when I worked on the test.

### How to run

To run the application, you just need to have the data files in the "Data" folder and then run the worker. The generated data will be saved in the application directory (...\LegislativeData) at the end of the processing.