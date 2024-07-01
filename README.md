# SimpleCrudApi

SimpleCrudApi is a CRUD API that interacts with a DynamoDB table. This API allows you to manage guitar objects, simulate cpu stress, and retrieve AWS availability zone information.

## Table of Contents

- [Getting Started](#getting-started)
- [Main Endpoints](#main-endpoints)
- [Other Endpoints](#other-endpoints)
  - [AZ Endpoint](#az-endpoint)
  - [Canary Endpoint](#canary-endpoint)
  - [Configuration Endpoint](#configuration-endpoint)
- [License](#license)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [AWS CLI](https://aws.amazon.com/cli/)
- AWS credentials configured for DynamoDB access
- A "Guitars" DynamoDB table with a partition key of "id"

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/SimpleCrudApi.git
    cd SimpleCrudApi
    ```

2. Restore dependencies and build the project:
    ```sh
    dotnet restore
    dotnet build
    ```

3. Run the application:
    ```sh
    dotnet run
    ```
## Main Endpoints

| Method   | URL                                      | Description                              |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/guitars`                           | Retrieve all guitars.                    |
| `GET`    | `/api/guitars/1`                         | Retrieve guitar with id 1.               |
| `POST`   | `/api/guitars`                           | Create a new guitar.                     |
| `PUT`    | `/api/guitars/2`                         | Update a guitar with id 2.               |
| `DELETE` | `/api/guitars/3`                         | Delete a guitar with id 3.               |

Request Body for POST and PUT:
- **Request Body**
    ```json
    {
      "id": 1,
      "make": "Fender",
      "model": "Stratocaster",
      "shape": "S",
      "strings": 6
    }
    ```
- **Response Body**
    ```json
    {
      "id": 1,
      "make": "Fender",
      "model": "Stratocaster",
      "shape": "S",
      "strings": 6
    }
    ```
### Status Codes
| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 201 | `CREATED` |
| 400 | `BAD REQUEST` |
| 404 | `NOT FOUND` |

    
## Other Endpoints
### AZ Endpoint

Retrieve AWS availability zone information.

- **GET** `/api/AZ`
  - **Response**: `eu-west-2a`

### Canary Endpoint

Retrieve canary information for testing purposes.

- **GET** `/api/Canary`
  - **Response**: `The SimpleCrudApi is running.`

### Configuration Endpoint

Simulate CPU stress for autoscaling purposes.

- **GET** `/api/Configuration/{durationInMinutes}`
  - **Parameters**:
    - `durationInMinutes` (integer, required): Duration in minutes
  - **Response**: `CPU stress test for {durationInMinutes} minute(s) complete.`

## License
This project is licensed under the MIT License. See the LICENSE file for details.