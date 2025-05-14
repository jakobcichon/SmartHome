from datetime import datetime, timedelta
import socket

message = b"IsThereSmartHomeServer?"
udp_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
udp_socket.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)


udp_socket.bind(('192.168.0.73', 54321))


udp_socket.sendto(message, ('255.255.255.255', 54321))

print("Broadcast message sent to the network.")

timeout = datetime.now() + timedelta(seconds=10)

while True:
  data, addr = udp_socket.recvfrom(1024)
  print (f"Received Data: {data}; From: {addr}")
  
  if datetime.now() > timeout:
    print("Timeout reached, stopping the broadcast.")
    break

udp_socket.close()
