-- Bảng users
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    display_name VARCHAR(100),
    avatar_url TEXT,
    created_at TIMESTAMP DEFAULT NOW()
);

-- Bảng conversations (1-1)
CREATE TABLE conversations (
    id SERIAL PRIMARY KEY,
    user1_id INT REFERENCES users(id) ON DELETE CASCADE,
    user2_id INT REFERENCES users(id) ON DELETE CASCADE,
    last_message TEXT,
    updated_at TIMESTAMP DEFAULT NOW(),
    UNIQUE(user1_id, user2_id)  -- tránh trùng conversation 1-1
);

-- Index để tìm nhanh theo user
CREATE INDEX idx_conversation_user1 ON conversations(user1_id);
CREATE INDEX idx_conversation_user2 ON conversations(user2_id);
CREATE INDEX idx_conversation_updated_at ON conversations(updated_at DESC);

-- ENUM cho loại tin nhắn
CREATE TYPE message_type AS ENUM ('text', 'image');

-- ENUM cho trạng thái tin nhắn
CREATE TYPE message_status AS ENUM ('sent', 'delivered', 'read');

-- Bảng messages
CREATE TABLE messages (
    id SERIAL PRIMARY KEY,
    conversation_id INT REFERENCES conversations(id) ON DELETE CASCADE,
    sender_id INT REFERENCES users(id) ON DELETE CASCADE,
    type message_type NOT NULL DEFAULT 'text',
    content TEXT NOT NULL,  -- text hoặc URL ảnh
    timestamp TIMESTAMP DEFAULT NOW(),
    status message_status DEFAULT 'sent'
);

-- Index để tìm nhanh tin nhắn
CREATE INDEX idx_message_conversation ON messages(conversation_id);
CREATE INDEX idx_message_timestamp ON messages(timestamp);

-- Bảng user_status
CREATE TABLE user_status (
    user_id INT PRIMARY KEY REFERENCES users(id) ON DELETE CASCADE,
    is_online BOOLEAN DEFAULT FALSE,
    last_seen TIMESTAMP DEFAULT NOW()
);

-- ENUM cho trạng thái bạn bè
CREATE TYPE friend_status AS ENUM ('pending', 'accepted', 'rejected');

-- Bảng friends
CREATE TABLE friends (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id) ON DELETE CASCADE,
    friend_id INT REFERENCES users(id) ON DELETE CASCADE,
    status friend_status DEFAULT 'pending',
    created_at TIMESTAMP DEFAULT NOW(),
    UNIQUE(user_id, friend_id)
);
