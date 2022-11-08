import sys
import math

start = "start"
end = "end"
onlygrabs = False
smoothing = 0.0

if len(sys.argv) >= 2 and (sys.argv[1] == "-h" or sys.argv[1] == "--help"):
    print("Usage: transformer.py PATH_IN PATH_OUT [options]")
    print("Options:\t '-startgrab' to only sample after the first grab")
    print("\t\t '-startrelease' to only sample after the first release")
    print("\t\t '-stopgrab' to only sample until the last grab")
    print("\t\t '-stoprelease' to only sample until the last release")
    print("\t\t '-smoothing x' to smooth samples together into buckets of x seconds")
    print("\t\t '-onlygrabs' to only extract grabs and releases")
    exit(0)

if len(sys.argv) < 3:
    print("Not enough arguments!")
    exit(-1)

infile = open(sys.argv[1], "r")
outfile = open(sys.argv[2], "a")

i = 3
while i < len(sys.argv):
    if sys.argv[i] == "-startgrab":
        start = "grab"
    elif sys.argv[i] == "-startrelease":
        start = "release"
    elif sys.argv[i] == "-stopgrab":
        stop = "grab"
    elif sys.argv[i] == "-stoprelease":
        stop = "release"
    elif sys.argv[i] == "-smoothing":
        i = i + 1
        smoothing = float(sys.argv[i])
    elif sys.argv[i] == "-onlygrabs":
        onlygrabs = True
    else:
        print("Unkown argument: %(arg)s" % {"arg": sys.argv[i]})
    i = i + 1

line = infile.readline()
#skip first two lines
line = infile.readline()
line = infile.readline()
 
if onlygrabs:
    while line:
        substrings = line.split(": ")
        if len(substrings) == 2:
            val = 0
            if substrings[1] == "Released object\n":
                val = 1
            outfile.write("{}, {}\n".format(substrings[0],val))
        line = infile.readline()
        
else:
    stackCount = 0
    smoothedValues = []
    lastTime = -math.inf

    while line:
        substrings = line.split(",")
        if len(substrings) == 6:
            if float(substrings[0]) - lastTime > smoothing:
                for i in range(len(smoothedValues)):
                    if i == 5:
                        outfile.write("{}\n".format(smoothedValues[i] / stackCount))
                    else:
                        outfile.write("{}, ".format(smoothedValues[i] / stackCount))
                smoothedValues = []
                for i in range(6):
                    smoothedValues.append(float(substrings[i]))
                    stackCount = 1
                    lastTime = smoothedValues[0]
            else:
                stackCount = stackCount + 1
                for i in range(6):
                    smoothedValues[i] = smoothedValues[i] + float(substrings[i])
        line = infile.readline()
    
    for i in range(len(smoothedValues)):
        if i == 5:
            outfile.write("{}\n".format(smoothedValues[i] / stackCount))
        else:
            outfile.write("{}, ".format(smoothedValues[i] / stackCount))

infile.close()
outfile.close()