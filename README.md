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

Click on Services, then click on EC2.

Click on "Launch Instance"

In the AMI selection screen, scroll down and select "Microsoft Windows Server 2016 Base" 

In the Instance Type screen, select a "t2.xlarge" instance, then click on "Review and Launch".

On the next screen, click on "Launch" and then select the "winhost" key pair you created earlier and launch the instance.

Click on "View Instances" to go to the list of your instances. 

Wait until the Instance State is running and Status Checks is green.

### Login To Windows Instance

From your desktop, launch RDP

In the Instance list view, copy the IPv4 Public IP for your instance and paste into the RDP host field.

Once RDP is connected, it will ask for the username and password.

Get the local administrator password for your instance by selecting your instance and in the "Actions" menu select "Get Windows Password"

In the pop-up window click on "Choose File" and select the key pair file that was downloaded earlier and click on "Decrypt Password".

Copy the password and login to the RDP session. You will now be logged in as the local administrator and ready to install Visual Studio. 

### Install and Configure Visual Studio Community Edition

Download Visual Studio Community 2017 from here: https://www.visualstudio.com/downloads/

Run the installer.

Launch Visual Studio Community 2017

### Install AWS Toolkit for Visual Studio

Once Visual Studio has loaded, let's install the AWS Toolkit for Visual Studio.

*Tools > Extensions and Updates...* menu command.

Click the 'Online' item in the left menu.

Search for 'AWS Toolkit' in the Search bar top right.

Select 'AWS Toolkit for Visual Studio 2017' and click 'Download.'

Once complete, close Visual Studio; the toolkit will be installed automatically.

Re-open Visual Studio.

*View > AWS Explorer* menu command.

### Configure AWS Toolkit for Visual Studio

Once the sidebar has loaded, click the 'New Account Profile' button.

In the modal windows, fill in the information: use 'default' as a profile name, and provide the access key ID and secret access key for the IAM profile created earlier.

Click OK.

From the 'Region' dropdown, select EU (London).

## Building the Unit Test Workflow

Now that we have our tools configured, let's get going. Since the purpose of this workshop is to demonstrate the flexibility of integrating the AWS platform with Microsoft workloads, let's use the PowerShell toolkit (pre-installed on this AWS Windows AMI) to spin up our Oracle RDS instance for testing our code.

### Validating our credentials

Open a PowerShell terminal.

Type the following command:

```powershell
Get-AWSCredential -ListProfileDetail
```

The result should be a list of configured AWS credentials, which will be limited the default profile we just created. 

### Database Creation

Now it's time to create our RDS instance. For this workshop, we'll create a small, Oracle RDS instance using the following PowerShell command: 

```powershell
NEW-RDSDBInstance -DBName oradb01 -AllocatedStorage 20 -DBInstanceIdentifier orainst01 -Engine oracle-se2 -EngineVersion 12.1.0.2.v9 -LicenseModel license-included -MasterUsername oraadmin -MasterUserPassword ******** -PubliclyAccessible $false -storagetype gp2 -DBInstanceClass db.m4.large -BackupRetentionPeriod 0
```

Once you've issued this command, check the creation task progress by:

```powershell
GET-RDSDBInstance | Select-Object DBInstanceStatus
```


### Configure Security Group


### Visual Studio Project Creation

In Visual Studio, click the *File > New > Project* menu command.

In the left sidebar, select *Visual C# > Test > Unit Test Project (.NET Framework)*

Give the project a name and location, then click 'OK.'

### Nuget Package Installation

For this project, we're going to need to install two Nuget Packages: one for NUnit, our unit test framework of choice, and one for the Oracle Data Provider.

*Tools > NuGet Package Manager > Package Manager Console* menu command.

In the Package Manager Console, run the following command:

```PowerShell
Install-Package Oracle.ManagedDataAccess -Version 12.2.1100
```
Wait for it to complete, then run:

```PowerShell
Install-Package NUnit -Version 3.10.1
```

Now that we've installed the necessary packages, we can dig into the code.

### Unit Test Code



### Database Teardown

```powershell
Remove-RDSDBInstance  -DBInstanceIdentifier $(instance-name) -SkipFinalSnapshot $true -Force
```
