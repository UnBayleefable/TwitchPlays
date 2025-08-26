import json
import sys
from queue import Empty
from threading import Thread
import time
import win32pipe, win32file, pywintypes

class WinformsController:
    def __init__(self, pipe_name):
        self.pipe_name = pipe_name
        self.handle = None
        
    def connect_to_pipe(self):
        try:
            self.handle = win32file.CreateFile(
                f'\\\\\\.\\pipe\\{self.pipe_name}',
                win32file.GENERIC_READ | win32file.GENERIC_WRITE,
                0, None, win32file.OPEN_EXISTING, 0, None)
            return True
        except pywintypes.error as e:
            print(f"Error connecting to pipe: {e}")
            return False
        
    def start_pipe_listener(self):
        self.pipe_thread = Thread(target=self.listen_for_messages)
        self.pipe_thread.daemon = True
        self.pipe_thread.start()
    
    def listen_for_messages(self):
        while self.is_running:
            try:
                # Read messages from pipe
                connection = win32pipe.ConnectNamedPipe(win32pipe.CreateNamedPipe(
                    r'\\.\pipe\twitch_plays_pipe',
                    win32pipe.PIPE_ACCESS_DUPLEX,
                    win32pipe.PIPE_TYPE_MESSAGE | win32pipe.PIPE_READMODE_MESSAGE | win32pipe.PIPE_WAIT,
                    1, 65536, 65536, 300, None))
                
                while self.is_running:
                    try:
                        message, _ = win32pipe.ReadFile(connection, 4096)
                        if message:
                            self.handle_message(json.loads(message.decode()))
                    except Exception as e:
                        print(f"Error reading from pipe: {e}")
                        break
                
            except Exception as e:
                print(f"Error connecting to pipe: {e}")
                time.sleep(1)
    
    def handle_message(self, message):
        command = message.get('command')
        if command == 'stop':
            self.is_running = False
            sys.exit(0)
        elif command == 'update_variables':
            # Update variables based on message content
            pass

def main():
    controller = WinformsController()
    controller.start_pipe_listener()
    
    # Your existing Twitch Plays logic here...
    while controller.is_running:
        # Process messages and handle game controls
        time.sleep(0.1)

if __name__ == "__main__":
    main()