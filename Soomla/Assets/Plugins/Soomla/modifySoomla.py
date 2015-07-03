import fileinput
import re
import argparse
import sys, os, fnmatch

class Manipulator:

    def __init__(self, args):
        self.verbose = args.verbose
        self.profiler = args.profiler
        self.remove = args.remove
        self.directory = args.directory
        self.input = args.inputfile
        self.output = args.outputfile
        self.list = args.list
        self.listOfFiles = []
        self.test = args.test;

    def run(self):
        if self.directory:
            self.handleDirectoryInput()
        else:
            self.handleFileInput()

        if self.test or self.list:
            print '\n'.join(self.listOfFiles)


    def addCompilerDirective(self, inputFile):
        return self.addStrings(
            inputFile, 
            '#if DEBUG_SOOMLA\n', 
            '#endif\n')

    def addProfiler(self, inputFile):
        return self.addStrings(
            inputFile, 
            'Profiler.BeginSample("LogDebug");\n', 
            'Profiler.EndSample();\n')

    def addStrings(self, inputFile, begin, end):
        if self.verbose:
            print "Reading input file: ", inputFile

        pattern = re.compile('(^.*)(SoomlaUtils.LogDebug.*)')

        out = ""
        carryOver = False
        alreadyHaveMatch = False

        hasPattern = False

        for line in fileinput.input(inputFile):
            if (carryOver):
                out += line
                if alreadyHaveMatch:
                    carryOver = end not in line
                else:
                    carryOver = not line.rstrip().endswith(';')
                if not carryOver:
                    if alreadyHaveMatch:
                        alreadyHaveMatch = False
                    else:
                        out += m.group(1)
                        out += end
                    carryOver = False
                continue

            m = pattern.match(line)

            if (m):
                carryOver = not line.rstrip().endswith(';')
                if alreadyHaveMatch:
                    carryOver = True
                else:
                    out += m.group(1)
                    out += begin
                    out += line
                    hasPattern = True;
                    if not carryOver:
                        out += m.group(1)
                        out += end
            else:
                alreadyHaveMatch = carryOver = begin in line
                out += line

        if hasPattern:
            self.listOfFiles.append(inputFile)
        return out

    def listFiles(self, inputFile):
        if self.verbose:
            print "Reading input file: ", inputFile

        data = open(inputFile, 'r').read()
        if data.find("SoomlaUtils.LogDebug") > -1:
            self.listOfFiles.append(inputFile)


    def removeCompilerDirective(self, inputFile):
        if self.verbose:
            print "Reading input file: ", inputFile

        pattern = re.compile('.*if DEBUG_SOOMLA')
        out = ""
        hasPattern = False

        foundLine = False

        for line in fileinput.input(inputFile):
            if foundLine:
                if "#endif" in line:
                    foundLine = False
                    continue

            m = pattern.match(line)
            if m:
                foundLine = True
            else:
                out += line

        if hasPattern:
            self.listOfFiles.append(inputFile)

        return out

    def removeProfiler(self, inputFile):
        if self.verbose:
            print "Reading input file: ", inputFile

        pattern = re.compile('.*Profiler.(Begin|End)')
        out = ""
        hasPattern = False

        for line in fileinput.input(inputFile):
            m = pattern.match(line)
            if m:
                hasPattern = True
            else:
                out += line

        if hasPattern:
            self.listOfFiles.append(inputFile)

        return out

    def writeFile(self, lines, outputFile):
        if self.verbose:
            print "Writing to output file: ", outputFile
            print 

        target = open(outputFile, 'w')
        target.write(lines)
        target.close()

    def processFile(self, inputFile, outputFile):
        if self.test: 
            out = self.listFiles(inputFile)
            what = None
        elif self.profiler:
            out = self.addProfiler(inputFile)
            what = "profiler"
        else:
            out = self.addCompilerDirective(inputFile)
            what = "directive"

        self.processOutput("Adding", what, inputFile, outputFile, out)


    def processOutput(self, action, what, inputFile, outputFile, out):
        if not self.test and self.verbose:
            print action, what, 'to ',inputFile, ' -> ', outputFile

        if not self.test:
            self.writeFile(out, outputFile)

    def processFileRemove(self, inputFile, outputFile):
        if self.test: 
            self.listFiles(inputFile)
            what = None
        elif self.profiler:
            out = self.removeProfiler(inputFile)
            what = "profiler"
        else:
            out = self.removeCompilerDirective(inputFile)
            what = "directive"

        self.processOutput("Removing", what, inputFile, outputFile, out)

    def handleDirectoryInput(self):
        path = ""
        if self.output:
            path = self.output

        for root, dirs, files in os.walk(self.input):

            for filename in fnmatch.filter(files, "*.cs"):
                if root == path:
                    continue

                inputFile = os.path.join(root, filename) 
                outputFile = os.path.join(path, inputFile)

                if not os.path.exists(os.path.dirname(outputFile)):
                    os.makedirs(os.path.dirname(outputFile))

                self.callProcess(inputFile, outputFile)

    def handleFileInput(self):
        inputFile = self.input
        if self.output:
            outputFile = self.output
        else:
            outputFile = inputFile

        self.callProcess(self.input, outputFile)


    def callProcess(self, inputFile, outputFile):
        if self.remove:
            self.processFileRemove(inputFile, outputFile)
        else:
            self.processFile(inputFile, outputFile)


def main(argv):
    parser = argparse.ArgumentParser()
    parser.add_argument("inputfile", help="file to read from")
    parser.add_argument("-d", "--directory", help="inputfile is a directory", action="store_true")
    parser.add_argument("-o", "--outputfile", help="file/directory to write to")
    parser.add_argument("-v", "--verbose", help="increase output verbosity", action="store_true")
    parser.add_argument("-p", "--profiler", help="add Profiler strings", action="store_true")
    parser.add_argument("-r", "--remove", help="remove the strings", action="store_true")
    parser.add_argument("-l", "--list", help="list out the files that change", action="store_true")
    parser.add_argument("-t", "--test", help="don't actually change any files", action="store_true")

    args = parser.parse_args()

    verbose = args.verbose

    manipulator = Manipulator(args)
    manipulator.run()

    if verbose:
        print "Finished"


if __name__ == "__main__":
    main(sys.argv[1:])
