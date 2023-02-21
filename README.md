# Tin-Can SVG Merge (tc-svg-merge)

Small program to merge multiple SVG files together by stacking them on a source image.

## Usage

```bash
tc-svg-merge <destination> <original> [[overlay1] [overlay2] [overlay3] ...]

```

## Methodology

Since SVG is just XML, the original file is loaded as an XmlDocument.
Overlay files are also loaded as XmlDocuments. 
The children of the overlay files' `svg` nodes are appended to the original document's `svg` node.
The resultant XML is then written to the destination file.

## Notes

Program returns 0 on clean exit, or a negative number otherwise

Program will overwrite the destination file if it exists.

Only tested with SVGs created in inkscape.
