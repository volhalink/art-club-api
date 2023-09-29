#!/bin/bash
mongosh -u $MONGO_INITDB_ROOT_USERNAME -p $MONGO_INITDB_ROOT_PASSWORD <<EOF
use admin;
apidb = db.getSiblingDB("${MONGO_API_DATABASE}");
apidb.createUser({ user: "${MONGO_API_USERNAME}", pwd: "${MONGO_API_PASSWORD}", roles: [ { role: "readWrite", db: "${MONGO_API_DATABASE}" } ] } );
apidb.createCollection("${MONGO_API_COLLECTION}");
EOF