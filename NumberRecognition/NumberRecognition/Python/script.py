import os
from multiprocessing import Process
import mmap

# Get the current working directory
current_dir = os.getcwd()

# Create a new directory in the current working directory
new_dir = os.path.join(current_dir, 'new_directory23423')
os.mkdir(new_dir)



exit(10);