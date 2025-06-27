
# StockManagement

StockManagement is a web and console application that helps you keep track of your home inventory. It allows you to manage your storage spaces, categorize your products, and receive alerts for missing items.

## Features

- **Storage Management**: Add and manage your storage spaces (rooms, furniture, etc.).
- **Product Categorization**: Organize your products into categories for better management.
- **Missing Product Alerts**: Creates alerts when your products reach critical levels.

## Installation

### Prerequisites

- .NET SDK installed on your machine.
- Access to an SQL database (SQL Server, SQLite, etc.).

### Installation Steps

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/Namularbre/StockManagement.git
   cd StockManagement
   ```

2. **Configure the Database**:

   - Navigate to the solution folder.
   - Add your database connection string to the `appsettings.json` files in both the `StockManagement` and `StockManagement_ConsoleApp` projects.

3. **Update the Database**:

   Run the following command to apply migrations and create the database:

   ```bash
   dotnet ef database update --project StockManagement_Persistance --startup-project StockManagement
   ```

   *Note: If you want to use the console application, replace `StockManagement` with `StockManagement_ConsoleApp` in the command above.*

4. **Build the Application**:

   Once the database is set up, you can build the application for your operating system:

   ```bash
   dotnet publish --sc --os <your_OS>
   ```

   Replace `<your_OS>` with your operating system (e.g., `linux-x64`, `win-x64`, etc.).

## Kubbernet Deployment

To deploy the application on a Kubernetes cluster, follow these steps:

First, ensure you have Docker and kubectl installed on your machine.
Then, add MS SQl Server on kubernetes with the following command:
```bash
git clone https://github.com/Namularbre/mssqlserver2022-k8s.git
cd mssqlserver2022-k8s
kubectl apply -f statefulset.yaml
```

*Note: Make sure to create a secret containing the sa password. **don't keep the one that is on this repository** !*

Then, you need to deploy the web application by running the following commands:
```bash
kubectl apply -f stockmanagement-web-deployment.yaml
kubectl apply -f stockmanagement-web-service.yaml
```

Here, if you still use the default database password, you can use the secret file:
```bash
kubectl apply -f stockmanagement-web-secret.yaml
```

Or run this command to create your own secret:
```bash
kubectl create secret generic stockmanagement-web-secret --from-literal=ConnectionStrings__DefaultConnection="Server=mssqlserver;Database=StockManagement;User Id=sa;Password=your_password_here;"
```
Finally, you can deploy the console application by running the following commands:
```bash
kubectl apply -f stockmanagement-console-cronjob.yaml
```

Again, you can use the secret file or create your own secret for the console application:
```bash
kubectl apply -f stockmanagement-console-secret.yaml
```

Remember that the secrets in the secret files are only examples. You should create your own secrets with your own passwords.

## Usage

- **Web Application**: Launch the web application for a user-friendly interface.
- **Console Application**: The console application need to be launched to create the alerts.

## Authors

- [Namularbre](https://github.com/Namularbre/)
