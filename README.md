### Description of the Program

The `QuoteController` is an ASP.NET Core REST API controller designed to manage quote-related operations. It interacts with a `IQuoteService` implementation to perform actions such as requesting, accepting, canceling, and retrieving quotes. It provides endpoints that enable clients to interact with the quoting system in a structured manner.

### Design Decisions

1. **Separation of Concerns**: 
   - The controller does not handle business logic directly; it delegates operations to the `IQuoteService`. This promotes a clear separation of concerns and allows for easier testing and maintenance.

2. **Async Programming**:
   - All operations are asynchronous, leveraging `async/await` to improve responsiveness and scalability of the API. This enables the application to handle more concurrent requests effectively, particularly important for I/O-bound tasks like database calls.

3. **RESTful API Design**:
   - The API follows REST principles by using appropriate HTTP verbs (POST for creating and modifying resources) and meaningful endpoint paths (e.g., `/request`, `/accept/{quoteId}`, `/cancel/{quoteId}`).

### Assumptions
- It is assumed that the API will be called with valid data when accepting or canceling quotes.

### System Limitations
- The "Confirm/Reject Quote" functionality may not be fully functional. This demo focuses on demonstrating code structure and API functionality rather than production-readiness.
- Error handling is limited to certain types of exceptions. Custom error handling or logging mechanisms could be implemented for better monitoring and debugging.

### API Endpoints

Below are the available API endpoints with request examples.

#### 1. Request a Quote

- **Endpoint**: `POST /api/Quote/request`
- **Request Body**:
  ```json
  {
    "Ticker": "AAPL",
    "Quantity": 10
  }
  ```

#### 2. Accept a Quote

- **Endpoint**: `POST /api/Quote/accept/{quoteId}`
- **Example Request**: `POST /api/Quote/accept/1`
- **Response**: 
  - Success: `"Quote accepted successfully."`
  - Not Found: `"Quote not found or already accepted."`

#### 3. Cancel a Quote

- **Endpoint**: `POST /api/Quote/cancel/{quoteId}`
- **Example Request**: `POST /api/Quote/cancel/1`
- **Response**: 
  - Success: `"Quote canceled successfully."`
  - Not Found: `"Quote not found."`

#### 4. Get All Quotes

- **Endpoint**: `GET /api/Quote/all`
- **Response**: A list of all quotes presented in JSON format.
