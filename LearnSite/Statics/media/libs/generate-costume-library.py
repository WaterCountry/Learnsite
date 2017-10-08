#!/usr/bin/env python
# vim: tabstop=8 expandtab shiftwidth=4 softtabstop=4

from StringIO import StringIO
import gzip
import json
import urllib2

cdn = 'cdn.assets.scratch.mit.edu'
#cdn = 'cdn.assets.scratch.ly'

def download(fileName):
    request = urllib2.Request('http://'+cdn+'/internalapi/asset/{0}/get/'.format(fileName))
    # print(request.get_full_url())
    request.add_header('Accept-encoding', 'gzip')
    response = urllib2.urlopen(request)
    contents = response.read()
    if response.info().get('Content-Encoding') == 'gzip':
        rawFile = StringIO(contents)
        gzFile = gzip.GzipFile(fileobj = rawFile)
        contents = gzFile.read()

    return contents

def processSprite(fileName, tags):
    sprite = json.loads(download(fileName))
    # with open(fileName) as f:
    #     sprite = json.load(f)

    output = []
    for costume in sprite["costumes"]:
        data = {
            "type": "costume",
            "name": costume["costumeName"],
            "tags": tags,
            "md5": costume["baseLayerMD5"],
            "info": [costume["rotationCenterX"], costume["rotationCenterY"]]
        }

        if not data["md5"].endswith("svg"):
            if "bitmapResolution" in costume:
                data["info"].append(costume["bitmapResolution"])
            else:
                data["info"].append(1)

        output.append(data)

    return output

def main():
    spriteLibFileName = "spriteLibrary.json"
    costumeLibFileName = "costumeLibrary.json"

    contents = []
    with open(spriteLibFileName) as f:
        library = json.load(f)
        for sprite in library:
            tags = sprite['tags']
            print "Processing " + sprite['name'] + "..."
            contents += processSprite(sprite['md5'], tags)

    with open(costumeLibFileName, 'wb') as f:
        f.write(json.dumps(contents, indent=4, separators=(',', ': ')))
        f.close()

if __name__ == "__main__":
    main()
