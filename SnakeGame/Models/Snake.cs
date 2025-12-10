using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
    internal class Snake
    {
        public List<Position> Body { get; private set; }
        public Direction.Huong CurrentDirection { get; private set; }
        
        // Input queue buffer - cho phép lưu tối đa 2 hướng
        private Queue<Direction.Huong> _directionQueue;
        private const int MaxQueueSize = 2;
        
        // Cache vectors để tránh tính toán lại
        private static readonly Dictionary<Direction.Huong, (int dx, int dy)> DirectionVectors = new Dictionary<Direction.Huong, (int dx, int dy)>
        {
            { Direction.Huong.Up, (0, -1) },
            { Direction.Huong.Down, (0, 1) },
            { Direction.Huong.Left, (-1, 0) },
            { Direction.Huong.Right, (1, 0) }
        };

        // Cache opposite directions để tối ưu kiểm tra
        private static readonly Dictionary<Direction.Huong, Direction.Huong> OppositeDirections = new Dictionary<Direction.Huong, Direction.Huong>
        {
            { Direction.Huong.Up, Direction.Huong.Down },
            { Direction.Huong.Down, Direction.Huong.Up },
            { Direction.Huong.Left, Direction.Huong.Right },
            { Direction.Huong.Right, Direction.Huong.Left }
        };

        public Snake(Position startPos, Direction.Huong startDirec)
        {
            Body = new List<Position> { startPos };
            CurrentDirection = startDirec;
            _directionQueue = new Queue<Direction.Huong>(MaxQueueSize);
        }

        // Đổi hướng di chuyển với input queue buffer
        public void ChangeDirection(Direction.Huong newDirection)
        {
            // Nếu queue rỗng, so sánh với CurrentDirection
            // Nếu queue có phần tử, so sánh với phần tử cuối trong queue
            Direction.Huong directionToCheck = _directionQueue.Count > 0 
                ? _directionQueue.Last() 
                : CurrentDirection;

            // Kiểm tra hướng mới có hợp lệ không
            if (!IsOppositeDirection(directionToCheck, newDirection) && newDirection != directionToCheck)
            {
                // Chỉ thêm vào queue nếu chưa đầy
                if (_directionQueue.Count < MaxQueueSize)
                {
                    _directionQueue.Enqueue(newDirection);
                }
            }
        }

        // Di chuyển rắn - tối ưu hóa
        public void Move()
        {
            // Lấy hướng tiếp theo từ queue nếu có
            if (_directionQueue.Count > 0)
            {
                CurrentDirection = _directionQueue.Dequeue();
            }

            // Lấy vector từ cache
            var vector = DirectionVectors[CurrentDirection];
            
            // Tính vị trí đầu mới
            var newHead = Body[0] + (vector.dx, vector.dy);
            
            // Thêm đầu mới
            Body.Insert(0, newHead);
            
            // Xóa đuôi (sẽ không xóa nếu vừa Grow)
            Body.RemoveAt(Body.Count - 1);
        }

        // Tăng chiều dài rắn - tối ưu
        public void Grow()
        {
            // Thêm segment mới tại vị trí đuôi hiện tại
            // Khi Move() được gọi, đuôi cũ sẽ không bị xóa nên rắn dài ra
            var tail = Body[Body.Count - 1];
            Body.Add(tail);
        }

        // Lấy vector di chuyển từ hướng - deprecated, dùng cache
        [Obsolete("Sử dụng DirectionVectors cache thay thế")]
        private (int dx, int dy) GetVector(Direction.Huong direction)
        {
            return DirectionVectors.ContainsKey(direction) 
                ? DirectionVectors[direction] 
                : (0, 0);
        }

        // Kiểm tra hướng mới có phải là hướng ngược lại không - tối ưu
        private bool IsOppositeDirection(Direction.Huong a, Direction.Huong b)
        {
            return OppositeDirections.ContainsKey(a) && OppositeDirections[a] == b;
        }

        // Method để clear queue khi cần (ví dụ: reset game)
        public void ClearDirectionQueue()
        {
            _directionQueue.Clear();
        }

        // Property để debug/test - kiểm tra số lượng hướng đang chờ
        public int PendingDirectionCount => _directionQueue.Count;
    }
}
