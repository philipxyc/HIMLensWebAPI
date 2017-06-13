# HIMLensWebAPI
HIMLens Web API is designed to binding tags to person by recognizing their face

### Create Person- Add a Person &amp; Tags to database

- JPEG, PNG, GIF(the first frame), and BMP are supported. The image file size should be larger than or equal to 1KB but no larger than 4MB.
- The detectable face size is between 36x36 to 4096x4096 pixels. The faces out of this range will not be detected.

#### Http Method

POST

#### Request URL

http://himlens.azurewebsites.net/create

#### Request headers

| Accept (optional) | string | Type of the response you want to receive from API. |
| --- | --- | --- |
| Ocp-Apim-Subscription-Key | string | Subscription key which provides access to this API. Found in your [Cognitive Services accounts](https://portal.azure.com/#blade/HubsExtension/BrowseResourceBlade/resourceType/Microsoft.CognitiveServices%2Faccounts). |
| Tags | string | Tags you want to add to this person. The first tag is recommended to be the name of the person. Each tag should be joined by semicolon. If Tags contain Chinese characters, the string should be UrlEncode. |
| RequestId | string | Designed to make sure the response match the present person. |

#### Request body

- [application/octet-stream](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#requesttab2)

 [binary data]

#### Response

List

| **First** | **Second** |
| --- | --- |
| RequestId | Success |

- [application/json](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#response200tab1)

{

    ["B8D802CF-DD8F-4E61-B15C-9E6C5844CCBA, Success"]

}

### Delete Person- Delete a Person &amp; all his/her Tags from database

- JPEG, PNG, GIF(the first frame), and BMP are supported. The image file size should be larger than or equal to 1KB but no larger than 4MB.
- The detectable face size is between 36x36 to 4096x4096 pixels. The faces out of this range will not be detected.

#### Http Method

DELETE

#### Request URL

http://himlens.azurewebsites.net/create

#### Request headers

| Accept (optional) | string | Type of the response you want to receive from API. |
| --- | --- | --- |
| Ocp-Apim-Subscription-Key | string | Subscription key which provides access to this API. Found in your [Cognitive Services accounts](https://portal.azure.com/#blade/HubsExtension/BrowseResourceBlade/resourceType/Microsoft.CognitiveServices%2Faccounts). |
| RequestId | string | Designed to make sure the response match the present person. |

#### Request body

- [application/octet-stream](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#requesttab2)

 [binary data]

#### Response

List

| **First** | **Second** |
| --- | --- |
| RequestId | Success |

- [application/json](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#response200tab1)

{

    ["B8D802CF-DD8F-4E61-B15C-9E6C5844CCBA, Success"]

}

### Add Tag - Add tags for a person who is already in database

- JPEG, PNG, GIF(the first frame), and BMP are supported. The image file size should be larger than or equal to 1KB but no larger than 4MB.
- The detectable face size is between 36x36 to 4096x4096 pixels. The faces out of this range will not be detected.

#### Http Method

POST

#### Request URL

http://himlens.azurewebsites.net/update

#### Request headers

| Accept (optional) | string | Type of the response you want to receive from API. |
| --- | --- | --- |
| Ocp-Apim-Subscription-Key | string | Subscription key which provides access to this API. Found in your [Cognitive Services accounts](https://portal.azure.com/#blade/HubsExtension/BrowseResourceBlade/resourceType/Microsoft.CognitiveServices%2Faccounts). |
| Tags | string | Tags you want to add to this person. Each tag should be joined by semicolon. If Tags contain Chinese characters, the string should be UrlEncode. |
| RequestId | string | Designed to make sure the response match the present person. |

#### Request body

- [application/octet-stream](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#requesttab2)

 [binary data]

#### Response

List

| **First** | **Second until the end** |
| --- | --- |
| RequestId | Added Tags |

- [application/json](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#response200tab1)

{

    ["B8D802CF-DD8F-4E61-B15C-9E6C5844CCBA, developer, ASP.NET"]

}

### Delete Tag - Delete tags for a person who is already in database

- JPEG, PNG, GIF(the first frame), and BMP are supported. The image file size should be larger than or equal to 1KB but no larger than 4MB.
- The detectable face size is between 36x36 to 4096x4096 pixels. The faces out of this range will not be detected.

#### Http Method

DELETE

#### Request URL

http://himlens.azurewebsites.net/update

#### Request headers

| Accept (optional) | string | Type of the response you want to receive from API. |
| --- | --- | --- |
| Ocp-Apim-Subscription-Key | string | Subscription key which provides access to this API. Found in your [Cognitive Services accounts](https://portal.azure.com/#blade/HubsExtension/BrowseResourceBlade/resourceType/Microsoft.CognitiveServices%2Faccounts). |
| Tags | string | Tags you want to delete for this person. Each tag should be joined by semicolon. If Tags contain Chinese characters, the string should be UrlEncode. |
| RequestId | string | Designed to make sure the response match the present person. |

#### Request body

- [application/octet-stream](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#requesttab2)

 [binary data]

#### Response

List

| **First** | **Second until the end** |
| --- | --- |
| RequestId | Deleted Tags |

- [application/json](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#response200tab1)

{

    ["B8D802CF-DD8F-4E61-B15C-9E6C5844CCBA, developer, ASP.NET"]

}

### Identify – Get all tags of a person who is already in database

- JPEG, PNG, GIF(the first frame), and BMP are supported. The image file size should be larger than or equal to 1KB but no larger than 4MB.
- The detectable face size is between 36x36 to 4096x4096 pixels. The faces out of this range will not be detected.

#### Http Method

POST

#### Request URL

http://himlens.azurewebsites.net/identify

#### Request headers

| Accept (optional) | string | Type of the response you want to receive from API. |
| --- | --- | --- |
| Ocp-Apim-Subscription-Key | string | Subscription key which provides access to this API. Found in your [Cognitive Services accounts](https://portal.azure.com/#blade/HubsExtension/BrowseResourceBlade/resourceType/Microsoft.CognitiveServices%2Faccounts). |
| RequestId | string | Designed to make sure the response match the present person. |

#### Request body

- [application/octet-stream](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#requesttab2)

 [binary data]

#### Response

List

| **First** | **Second until the end** |
| --- | --- |
| RequestId | Tags |

- [application/json](../../C:%5CUsers%5Cphili%5CDownloads%5CCognitive%20Services%20APIs%20Reference.htm#response200tab1)

{

    ["B8D802CF-DD8F-4E61-B15C-9E6C5844CCBA, developer, ASP.NET"]

}
