---
title: "How to use the GeodeticDatum microservice?"
output: html_document
---

Typical Usage
===
1. Upload a new GeodeticDatum using the `Post` web api method.
2. Call the `Get` method with the identifier of the uploaded GeodeticDatum as argument. 
The return Json object contains the GeodeticDatum description.
3. Optionally send a `Delete` request with the identifier of the GeodeticDatum in order to delete the GeodeticDatum if you do not 
want to keep the GeodeticDatum uploaded on the microservice.


