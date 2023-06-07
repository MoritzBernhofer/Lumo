import cv2
import socket

# Define the server address and port
server_address = '10.0.0.201'  # Replace with the IP address of your laptop
server_port = 12345

# Create a socket and connect to the server
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
try:
    sock.connect((server_address, server_port))
    print("Connected to the server:", server_address, "on port:", server_port)
except Exception as e:
    print("Error connecting to the server:", e)
    exit(1)

# Capture and send frames
cap = cv2.VideoCapture(0)

while True:
    # Capture a frame from the camera
    ret, frame = cap.read()

    # Check if the frame is valid
    if ret and frame is not None and frame.shape[0] > 0 and frame.shape[1] > 0:
        try:
            # Encode the frame as a JPEG image
            _, img_encoded = cv2.imencode('.jpg', frame)
        except cv2.error as e:
            print("Error encoding the frame:", e)
            continue
    else:
        print("Invalid frame or empty frame received")
        continue

    # Send the encoded frame to the server
    try:
        sock.sendall(img_encoded.tobytes())
    except Exception as e:
        print("Error sending the frame:", e)
        break

# Clean up
cap.release()
sock.close()
