# ActivityHub.Net
*WIP but Working*

### A C# .Net Implimentation of ActivityPub Entity Types
https://www.w3.org/TR/activitypub/

## Features
- [x] Immutable Implementation of ActivityPub.Link and ActivityPub.Object Base Class Types.
- [x] Implementation of Singular and Plural Versions of most Standard ActivityPub Fields.
    - Such as Rel vs Rels.
- [x] Implementation of Language Mappable Fields.
    - Such as nameMap.
- [x] Full ActivityPub Objects Json Serialization and Deserialization.
- [x] Support for Field Values of Array or Single Object during Deserialization of Compatable Fields.
- [x] Support for Link objects that are just Text Strings.
    - This occurs during serialization if the Link object only contains the Default Type and MediaType fields as well as an Href.

## TODO
- [ ] Unit Tests
- [ ] Open Source Licencing
- [ ] Impliment Classes for ALL Valid ActivityPub Entity Types.
- [ ] Test Compatability with EFCore.
- [ ] Impliment Extra Functionality of Types; Such as Action Creation.

## Simple Usage Example
```
Settings.DefaultContext = new Link("ActivityPub.Net.Testing");

Object testObject = new Object {
    Type = "Test",
    At = new Link("/terry") {
        Rels = new string[] {
            "test",
            "test2"
        }
    },
    Attribution = "/meep",
    Audience = new Link("/all") {
        Rel = "test"
    }
};

string json = testObject
    .Serialize();

Object @object
    = json.DeSerializeEntity<Object>();
```
