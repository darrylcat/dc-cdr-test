# dc-cdr-test
Demo  project for DWS

The technology used is .net 5 as that's the lastest version Visual Studio 2019 can cope with.

I'm assuming that Authorize and Authorize polices will be used, and that the middleware will automatically populate base user, claims and roles.

There are two Api Endpoints: 
	FileUpload which handles the import of data.
	ReportingApi which handles the reporting of data.

There's only one reporting function List which takes the Subscribers Number and a date range.

Additional testing: Set up postman to perform end to end testing.

Can the solution be deployed? Yes. But please don't.

Things to do:

1) Add in user and client tables.
2) Add in client criteria to the reporting and importing functions.
3) Add in the authorize and authorize policies.
4) Refactor the code so that save the data to disk and importing the data into the database are performed by seperate service classes.
5) Then refactor the code so that the import is carried about by a seperate windows service (think n-tier design).

Things not to do:
1) Refactor database model into a shared library. The scope of access for each service endpoint should be kept to a minimum.
