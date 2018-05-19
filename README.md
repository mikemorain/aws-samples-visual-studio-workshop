# .NET / Oracle Workshop
## Initial Setup

Login at aws.amazon.com with your user name and password.

### Configure IAM User

Click on Services, then click on IAM

On the left-hand side menu, click on Users

Click the "Add User" button, then type "rds-api-user" and select "Programmatic access" in the Access Type section.

Then, click on the "Next: Permissions" button

In the next window, click on "Attach existing policies directly"

In the Policy type search box, type RDS then select the AmazonRDSFullAccess policy

Scroll down and click on the "Next:Review" button, then click "Create user" and wait for the next screen

Click on "Download .csv" so that you can save the credentials. The credentials will no longer be available once you close this screen.

### Create EC2 Key Pair

Click on Services, then click on EC2.

On the left-hand side menu, click on "Key Pairs"

Click on the "Create Key Pair" button, type in a name, i.e. 'winhost' then click on "Create"

The key will be automatically downloaded to your computer. Please copy and place on your desktop

## Windows Instance Setup
### Launch Windows Instance
### Login To Windows Instance
### Install and Configure Visual Studio Community Edition

*Tools > NuGet Package Manager > Package Manager Console* menu command.

Run: Install-Package Oracle.ManagedDataAccess -Version 12.2.1100

*Tools > NuGet Package Manager > Package Manager Console* menu command.

Run: Install-Package NUnit -Version 3.10.1

### Configure Windows Powershell for AWS

## Workflow Configuration
### Database Creation
### Configure Security Group
### Visual Studio Project Creation
### Unit Test Code
### Database Teardown
