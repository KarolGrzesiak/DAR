# DAR - Bachelor Thesis 
## Design and Implementation of Software for Process and Decision Models Generation based on Attribute Relationship Diagrams
*\***\***\****Important***\***\***\** *Code here is definitely not production ready, nor I aimed it to be, the thesis was more about providing the proof of concept and the time was heavily limited. Overengineering is the worst.* *\***\***\****Important***\***\***\**

[Quick presentation of the application](https://youtu.be/P9SBUGK77WY)   
[Documentation - Thesis [PL] - Chapters 3 and 4 include a lot more technical stuff (e.g. architecture)](https://github.com/KarolGrzesiak/DAR/blob/master/Doc/grzesiak.pdf)  
*Swagger API available after launching application* 

## Description
The thesis presents a design and implementation of the **"DAR"** web application, which is a generator of business processes models in **BPMN** notation and decisions in **DMN** notation based on specifications in a form of **HML** files, describing attribute relationship diagrams **ARD**. Created system has been integrated with **Camunda** environment which supports business process management and decision evaluation by providing workflow and decision engines. Thanks to that, it is possible to generate an appropriate process model and then deploy it on the described environment in order to launch it and observe the results. The user has an ability to supplement a necessary information in a form of business rules in the created **DMN** models, which results in the evaluation of these decisions in a running process. The paper explains terms related to business processes and describes the process of creating mentioned application, both from the design and implementation side.

Key technologies:
- **ASP.NET Core** 
- **VueJS** 
- **Simple injector** 
- **EF Core** 
- **Camunda Workflow&Decision**
