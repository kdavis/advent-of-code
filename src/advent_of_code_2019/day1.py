"""
Day 1 of Advent Of Code for 2019
"""
import math
import os

def sum_of_fuel_requirements():
    """
    Calculate the sum of fuel requirements to launch
    """
    fuel_requirement = 0

    for line in open(os.path.dirname(__file__) + os.path.sep + "inputs/day1input.txt", "r").readlines():
        # Convert each line to an integer as it's stored as a string in a text file
        mass = int(line)
        fuel_requirement += _calculate_fuel_requirement(mass)

    return fuel_requirement

def calculate_fuels_fuel_requirements():
    total_fuel_requirement = 0

    for line in open(os.path.dirname(__file__) + os.path.sep + "inputs/day1input.txt", "r").readlines():
        # Convert each line to an integer as it's stored as a string in a text file
        mass = int(line)

        current_module_fuel_requirement = 0
        last_fuel_requirement = mass

        # Calculate the fuel requirement for the fuel needed for the mass
        while True:
            last_fuel_requirement = _calculate_fuel_requirement(last_fuel_requirement)
            if last_fuel_requirement <= 0:
                break
            current_module_fuel_requirement += last_fuel_requirement
        
        total_fuel_requirement += current_module_fuel_requirement

    return total_fuel_requirement

def _calculate_fuel_requirement(mass):
    """
    Helper function to calculate mass
    """
    return math.floor(mass / 3) - 2

if __name__ == "__main__":
    fuel_requirement = sum_of_fuel_requirements()
    print(f"The fuel required to launch is: {fuel_requirement}")

    mass_fuel_and_fuel_requirements = calculate_fuels_fuel_requirements()
    print(f"The fuel required to launch with extra fuel is: {mass_fuel_and_fuel_requirements}")
