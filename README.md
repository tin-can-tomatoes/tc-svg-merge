# Tin-Can SVG Merge (tc-svg-merge)

Small program to merge multiple SVG files together by stacking them on a source image.

## Usage

```bash
tc-svg-merge <destination> <original> [[overlay1] [overlay2] [overlay3] ...]

```

## Notes

Program returns 0 on clean exit, or a negative number otherwise

Program will overwrite the destination file if it exists.

Only tested with SVGs created in inkscape.
