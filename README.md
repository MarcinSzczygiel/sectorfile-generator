# Sectorfile generator

## Preface
When I create [EuroScope](http://www.euroscope.hu/) [sectorfile](https://github.com/MarcinSzczygiel/EPWW-Euroscope-sectorfile) for next [AIRAC](https://en.wikipedia.org/wiki/Aeronautical_Information_Publication) cycle, I usually have to do a bunch of data conversions. The main among them is the conversion of coordinates.

[AIP Poland](http://ais.pansa.pl/aip/) and it's mirror at [Eurocontrol](http://www.ead.eurocontrol.int/publicuser/public/pu/login.jsp) (both available upon free registration) provides coordinates in two main formats:
```
51°16'13"N 022°37'54"E
```
or
```
51°15'40''N 022°33'48''E
```
They are both similar, the only difference is in longitude and latitude seconds designators.
EuroScope also has to be provided with coordinates in two formats. Instead of single point coordinates, here we use mainly lines, which are described by two ending points, each of them with his own coordinates:

.ESE format:
```
COORD:N051.16.13.000:E022.37.54.000
COORD:N051.19.58.000:E022.47.40.000
```

.SCT format:
```
N051.16.13.000 E022.37.54.000 N051.19.58.000 E022.47.40.000
```

Both of them describe the same line. Multiple sections are arranged into polygons and, after describing its vertical limits, into the airspace chunks. Polygons can be open or closed, depending on certain situation.

## Nearby goal
First of all, this application has to help me with conversion of coordinates into correct format.
For now, this goal has been reached. User can paste list of coordinates with metadata into left panel of GUI and get converted data from right panel.

## Future goals
The main target is to create application which helps in all steps of editing the sectorfile. Here are example requirements:
* draw lines and curves with given parameters (e.g. draw a quarter of circle around given point with radius of 2 nautical miles)
* allow user to visually create and manage lines, especially for airport infrastructure visualization,
* round polygons with given radius for SIR and STAR procedures visualization,
* store sectorfile in easy managed format (XML?) and convert it to .SCT and .ESE on demand.

