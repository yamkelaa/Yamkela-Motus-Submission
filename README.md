In this project, I used a CQRS (Command Query Responsibility Segregation) architecture to separate the responsibilities for commands and queries at the backend. I organized the project structure by dividing the code into two main folders: one for commands and the other for queries. This separation helped ensure better maintainability and scalability.

Commands: I used commands for actions such as create, update, and delete, which return either a success response or an error message.
Queries: Queries were responsible for retrieving data, and in cases where data was not found, I returned an empty object instead of null to reduce complexity on the frontend. This decision was made after evaluating a couple of approaches, as returning an empty object seemed simpler and more predictable for the frontend.
Frontend (Angular)
For the frontend, I used Angular with TypeScript. I implemented NgRx for state management, which allowed me to store the application state and handle communication between components through actions and selectors. I also relied on services to fetch data and update the state.

Services handled data fetching and business logic.
NgRx was used for managing application state and providing a reactive data flow.
Toastr notifications were employed to communicate feedback to users (e.g., success, errors).
UI and Styling
I built the UI using Bootstrap for responsive design and layout, while also adding some custom styling to align with the branding and specific visual requirements. My goal was to maintain a clean, intuitive interface that was both functional and visually appealing.

Core Features
Add, Edit, Delete Functionalities: These operations were implemented using commands to ensure that the logic for performing actions was separated from the logic for retrieving data.
Querying Data: For displaying data, I used queries. I made the decision to return an empty object instead of null when no data was found, which made it easier to handle the response on the frontend without needing extra checks for null.
Key Design Decisions
Empty Object vs Null: Instead of returning null for missing data, I returned an empty object. This approach simplified the frontend code by eliminating the need to check for null values, making the code more concise and easier to understand.

Dynamic Feedback: I used Toastr notifications to provide dynamic feedback to the user. This allowed me to show success, error, or loading messages in a non-intrusive way, improving the overall user experience.

Technologies Used
Backend:
CQRS Architecture (Separation of commands and queries)
REST API (for frontend-backend communication)
Frontend:
Angular (for building the user interface)
NgRx (for state management)
TypeScript (for type safety and better tooling)
Bootstrap (for responsive design)
Toastr (for user feedback notifications)
Potential Future Improvements
If I had more time, I would have liked to implement the following enhancements:


Azure Deployment: I would have explored deploying the application on Azure, which would provide scalability and easier management of the application in a cloud environment.

Conclusion
The goal of this project was to build a clean, efficient application while leveraging the CQRS pattern to ensure clear separation between command and query operations. I aimed to simplify the frontend by reducing complexity and avoiding unnecessary checks for null. The dynamic feedback via Toastr notifications and the use of NgRx for state management helped create a smooth user experience. If I had more time, I would focus on adding tests and improving deployment strategies to make the application even more robust and scalable.
