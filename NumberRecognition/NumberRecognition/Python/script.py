import os

# Get the current working directory
current_dir = os.getcwd()

# Create a new directory in the current working directory
new_dir = os.path.join(current_dir, 'new_directory')
os.mkdir(new_dir)