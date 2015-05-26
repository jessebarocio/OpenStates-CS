# OpenStates-CS

A .NET wrapper for the Sunlight Foundation Open States API ([http://openstates.org/api](http://openstates.org/api)).

## Important Notes

### This library is incomplete!

I created this wrapper for another project I'm working on. It does not currently implement all of the available methods in the Open States API. I'm implementing them as needed.

### Inconsistent data

The Open States data comes from scraping the websites of various state legislatures. Scraping a website for data is very brittle so there will be mistakes and inconsistencies.

## Basic Usage Examples

You will need an API key to work with the Open States API. One can be obtained from [http://sunlightfoundation.com/api/](http://sunlightfoundation.com/api/).

    using(var client = new OpenStatesClient("YourOpenStatesApiKey"))
    {
		// You can search for legislators using various parameters. All parameters are optional.
		// This example searches for all Republican members of the Utah State Senate.
        var republicanSenators = client.LegislatorSearch(
			state: State.UT,
			chamber: Chamber.Upper,
			party: "Republican"
		);

		// Retrieve legislators who are serving districts containing the given coordinates. 
		var legislatorsByCoordinates = client.LegislatorsGeoLookup(41.082303, -111.996914);

		// Retrieve information on all Utah Senate districts.
		var senateDistricts = client.DistrictSearch(State.UT, Chamber.Upper);

		// Retrieve detailed geographical boundary information for the given district.
		var districtBoundary = client.DistrictBoundaryLookup("sldl/ut-22");
    }


## What's left to do

- ~~Metadata methods~~
- Bill methods
- ~~Legislator methods~~
- ~~Committee methods~~ (thanks @pcopley)
- Event methods
- ~~District methods~~

## NuGet

This package is available on NuGet.

    Install-Package OpenStates-CS