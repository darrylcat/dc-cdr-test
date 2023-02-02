# dc-cdr-test
Demo  project for DWS

The technology used is .net 5 as that's the lastest version Visual Studio 2019 can cope with.

I'm assuming that Authorize and Authorize polices will be used, and that the middleware will automatically populate base user, claims and roles.

There are two Api Endpoints: 
	FileUpload which handles the import of data.
	ReportingApi which handles the reporting of data.

There's only one reporting function List which takes the Subscribers Number and a date range.



